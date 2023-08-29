const axios = require('axios');

const baseUrl = 'http://localhost:7129';

const post = async (route, data, options) => {
  const response = await axios.post(`${baseUrl}${route}`, data);
  const { data: response_data } = response;

  if (response_data) {
    const { code, data, msg } = response_data;
    if (code) {
      throw new Error(msg);
    }

    return data;
  } else {
    throw new Error('NETWORK ERROR');
  }
};

module.exports = { post };
