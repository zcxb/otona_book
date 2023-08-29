using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtonaBookApi.Controllers;
using OtonaBookApi.Common;
using Minio;
using System.Net.Mime;
using System.Security.AccessControl;


namespace OtonaBookApi.Areas.Common
{
    [Route("common")]
    public class CommonController : AreaControllerBase
    {
        static string endpoint = "localhost:9000";
        static string accessKey = "TmbB3PnPWseV5TwMlEe2";
        static string secretKey = "QeUdR3hUmbu1TxFYlVdek4mCHZHovRDX9n77KYES";
        static bool secure = false;

        private static MinioClient minio = new MinioClient()
                                            .WithEndpoint(endpoint)
                                            .WithCredentials(accessKey, secretKey)
                                            .WithSSL(secure)
                                            .Build();

        [HttpPost("upload-file")]
        public async Task<ResponseResult<object>> UploadFile(
            [FromForm] IFormFile? file,
            [FromQuery] string imgPath)
        {
            if (file == null)
            {
                return Ok(null);
            }

            var fStream = file.OpenReadStream();
            var objectName = Path.GetFileName(imgPath);
            var putObjectArgs = new PutObjectArgs()
                .WithBucket("otona-book")
                .WithObject(imgPath)
                .WithObjectSize(fStream.Length)
                .WithStreamData(fStream);
            var response = await minio.PutObjectAsync(putObjectArgs);
            return new { file_path = imgPath };
        }
    }
}
