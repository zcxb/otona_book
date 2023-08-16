using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OtonaBookApi.Common
{
    public enum ResponseResultCode
    {
        [Description("请求成功")]
        SUCCESS = 0,
        [Description("请求失败")]
        ERROR = -1,
    }

    public class ResponseResult<T>
    {
        [JsonPropertyName("code")]
        public ResponseResultCode Code { get; set; }

        [JsonPropertyName("sub_code")]
        public string? SubCode { get; set; }

        [JsonPropertyName("msg")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("sub_msg")]
        public string? SubMessage { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public static implicit operator ResponseResult<T>(T value)
        {
            return new ResponseResult<T>
            {
                Code = ResponseResultCode.SUCCESS,
                Message = ResponseResultCode.SUCCESS.ToString(),
                Data = value
            };
        }
    }
}

