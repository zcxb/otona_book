const { createPage } = require('../../utils/browser');
const dayjs = require('dayjs');

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

    let released_url = `${baseUrl}/cn/released`;
    if (page_no !== 1) {
      released_url += `/page/${page_no}`;
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

      try {
        const [movie_box_href] = await page.$x(`//div[@class='item'][${index + 1}]//a[@class='movie-box']/@href`);
        let item_href = await page.evaluate((el) => el.value, movie_box_href);
        if (item_href.startsWith('//')) {
          item_href = 'http:' + item_href;
        }

        const detail_page = await createPage(browser);
        const item_detail = await Avmoo.collect_item(detail_page, { url: item_href, referer: released_url });
        if (detail_page) {
          await detail_page.close();
          detail_page = null;
        }
        await Avmoo.save_item({ item_detail });
      } catch (error) {}
    }

    return true;
  }

  static async collect_item(page, { url, referer }) {
    if (!url) {
      return null;
    }

    try {
      await page.goto(url, {
        timeout: 60000,
        referer,
      });
    } catch (error) {}
  }

  static async save_item(item) {
    const { item_detail } = item;

    console.log('yes, we upload this item!');
  }
}

module.exports = Avmoo;
