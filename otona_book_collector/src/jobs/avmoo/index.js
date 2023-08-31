const { createPage } = require('../../utils/browser');
const dayjs = require('dayjs');
const axios = require('axios');
const http = require('http');
// const { uploadBuffer } = require('../../utils/api');
const FormData = require('form-data');
var parseUrl = require('url').parse;
const agent = new http.Agent({ rejectUnauthorized: false });
const otona_api_client = require('./api-client');

const uploadBuffer = (url, data, options) => {
  try {
    return new Promise((resolve, reject) => {
      let formData = new FormData({
        httpsAgent: agent,
      });
      if (data) {
        for (const key in data) {
          const value = data[key];
          if (typeof value === 'object' && Buffer.isBuffer(value)) {
            formData.append(key, value, { knownLength: value.length, filename: key });
            continue;
          }
          formData.append(key, value);
        }
      }

      const params = parseUrl(url);
      params.headers = {
        'Content-Type': `multipart/form-data; boundary=${formData.getBoundary()}`,
      };
      params.agent = agent;
      formData.submit(params, function (err, response) {
        if (err) {
          reject(err);
        }

        response.resume();

        let rawData = '';
        response.on('data', (chunk) => {
          rawData += chunk;
        });
        response.on('end', () => {
          try {
            const parsedData = JSON.parse(rawData);
            resolve(parsedData);
          } catch (e) {
            reject(e);
          }
        });
      });
    });
  } catch (error) {
    console.log(error);
  }
};

const baseUrl = 'https://avmoo.cfd';

class Avmoo {
  static async run(browser) {
    let page = null;
    try {
      page = await createPage(browser);
      //   await page.goto(`${baseUrl}/cn`);

      await Avmoo.collect_released(browser, page);
    } catch (error) {
      console.error(error.message);
    } finally {
      if (page) {
        await page.close();
        page = null;
      }
    }
  }

  static async collect_released(browser, page) {
    let page_no = 1;
    let end_flag = true;

    while (end_flag) {
      end_flag = await Avmoo.collect_page(browser, page, page_no++);
    }
  }

  static async collect_page(browser, page, page_no) {
    const last_date = '2023-08-19';

    let released_url = `${baseUrl}/ja/released`;
    if (page_no !== 1) {
      released_url += `/page/${page_no}`;
    }

    let cover_imgs = new Map();
    if (!page.listenerCount('response')) {
      await page.on('response', async (response) => {
        const url = response.url();
        if (url.includes('/digital/video') && 200 === response.status()) {
          const url_segments = url.split('/');
          const image_name = url_segments[url_segments.length - 1];
          const film_id = url_segments[url_segments.length - 2];
          const image_buf = await response.buffer();
          cover_imgs.set(film_id, { image_name, image_buf });
        }
      });
    }
    await page.goto(released_url, {
      waitUntil: 'networkidle2',
      timeout: 60000,
    });

    const items = await page.$x(`//div[@class='item']`);
    for (let index = 0; index < items.length; index++) {
      const [item_published_at] = await page.$x(`//div[@class='item'][${index + 1}]//span/date[2]`);

      if (!item_published_at) {
        throw new Error('无法找到元素 published_at');
      }

      const published_at = await page.evaluate((el) => el.innerText, item_published_at);
      if (dayjs(published_at).isSame(last_date)) {
        return false;
      }

      const [item_bango] = await page.$x(`//div[@class='item'][${index + 1}]//span/date[1]`);
      const [item_title] = await page.$x(`//div[@class='item'][${index + 1}]//div[@class='photo-frame']/img/@title`);
      const [item_imgsrc] = await page.$x(`//div[@class='item'][${index + 1}]//div[@class='photo-frame']/img/@src`);
      const bango = await page.evaluate((el) => el.innerText, item_bango);
      const title = await page.evaluate((el) => el.value, item_title);
      const imgsrc = await page.evaluate((el) => el.value, item_imgsrc);

      console.log(`${bango} / ${published_at}`);
      console.log(title);
      console.log(imgsrc);

      let cover = null;
      const imgsrc_splited = imgsrc.split('/');
      const cover_key = imgsrc_splited[imgsrc_splited.length - 2];
      if (cover_key) {
        if (cover_imgs.has(cover_key)) {
          cover = cover_imgs.get(cover_key);
        }
      }

      try {
        const [movie_box_href] = await page.$x(`//div[@class='item'][${index + 1}]//a[@class='movie-box']/@href`);
        let item_href = await page.evaluate((el) => el.value, movie_box_href);
        if (item_href.startsWith('//')) {
          item_href = 'http:' + item_href;
        }
        console.log('item_href: ', item_href);

        const detail_page = await createPage(browser);
        const item_detail = await Avmoo.collect_item(detail_page, {
          cover,
          bango,
          url: item_href,
          referer: released_url,
        });
        await Avmoo.save_item({ ...item_detail, published_at, title });
        if (detail_page) {
          await detail_page.close();
          detail_page = null;
        }
      } catch (error) {}
    }

    return true;
  }

  static async collect_item(page, { cover, bango, url, referer }) {
    if (!url) {
      return null;
    }

    const sample_images = [];
    await page.on('response', async (response) => {
      const url = response.url();
      if (url.includes('/digital/video') && url.includes('-') && 200 == response.status()) {
        const url_segments = url.split('/');
        const image_name = url_segments[url_segments.length - 1];
        const image_buf = await response.buffer();
        sample_images.push({ image_name, image_buf });
      }
    });

    const film_item = {
      bango,
      cover_images: [],
      sample_images: [],
      actress: [],
      tags: [],
    };

    try {
      await page.goto(url, {
        waitUntil: 'networkidle2',
        timeout: 60000,
        referer,
      });

      if (cover) {
        const { image_name, image_buf } = cover;
        const img_path = `/films/img/${bango}/${image_name}`;

        const { code, data, msg } = await uploadBuffer(Avmoo.concatUploadPath(img_path), { file: image_buf });
        if (code) {
          throw new Error(msg);
        }
        if (data) {
          const { file_path } = data;
          film_item.cover_images = [file_path];
        }
      }

      const actress_name_spans = await page.$x(`//div[@id='avatar-waterfall']//span`);
      for (const actress_name_span of actress_name_spans) {
        const actress_name = await page.evaluate((el) => el.innerText, actress_name_span);
        if (actress_name) {
          film_item.actress.push(actress_name);
        }
      }

      const tag_els = await page.$x(`//span[@class='genre']/a`);
      for (let index = 0; index < tag_els.length; index++) {
        const tag_name = await page.evaluate((el) => el.innerText, tag_els[index]);
        const [tag_href_attr] = await page.$x(`//span[@class='genre'][${index + 1}]/a/@href`);
        const tag_href = await page.evaluate((el) => el.value, tag_href_attr);
        const tag_href_splited = tag_href.split('/');
        const tag_uid = tag_href_splited[tag_href_splited.length - 1];
        film_item.tags.push({ tag_name, tag_uid });
      }

      for (const sample_image of sample_images) {
        const { image_name, image_buf } = sample_image;

        try {
          const form = new FormData();
          form.append('file', image_buf, { knownLength: image_buf.length, filename: 'file' });
          const img_path = `/films/img/${bango}/samples/${image_name}`;

          const { code, data, msg } = await uploadBuffer(Avmoo.concatUploadPath(img_path), { file: image_buf });
          if (code) {
            throw new Error(msg);
          }
          if (data) {
            const { file_path } = data;
            film_item.sample_images.push(file_path);
          }
          // TODO:
        } catch (error) {
          console.error(error);
        }
      }
    } catch (error) {}
    return film_item;
  }

  static concatUploadPath(img_path) {
    return `http://localhost:7129/common/upload-file?imgPath=${img_path}`;
  }

  static async save_item(item) {
    try {
      const res = await otona_api_client.post('/film/save-film-item', item);
    } catch (error) {
      console.log(error);
    }
    console.log('yes, we upload this item!');
  }
}

module.exports = Avmoo;
