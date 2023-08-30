const axios = require('axios');

const baseUrl = 'http://localhost:7129';

const post = async (route, data, options) => {
  const response = await axios.post(`${baseUrl}${route}`, data);
  const { data: response_data } = response;

  if (response_data) {
    const { code, data, msg, sub_code, sub_msg } = response_data;
    if (code) {
      let error_msg = msg;
      if (sub_code && sub_msg) {
        error_msg += ` ### [${sub_code}] ${sub_msg}`;
      }
      throw new Error(error_msg);
    }

    return data;
  } else {
    throw new Error('NETWORK ERROR');
  }
};

module.exports = { post };
