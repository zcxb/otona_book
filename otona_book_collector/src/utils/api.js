const FormData = require('form-data');

const uploadBuffer = (url, data, options) => {
  return new Promise((resolve, reject) => {
    let formData = new FormData();
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
    formData.submit(params, function (err, response) {
      if (err) {
        throw err;
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
};

module.exports = { uploadBuffer };
