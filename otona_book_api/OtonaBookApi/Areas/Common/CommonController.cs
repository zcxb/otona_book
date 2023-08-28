using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtonaBookApi.Controllers;
using FluentFTP;
using OtonaBookApi.Common;

namespace OtonaBookApi.Areas.Common
{
    [Route("common")]
    public class CommonController : AreaControllerBase
    {
        [HttpPost("upload-file")]
        public ResponseResult<object> UploadFile(
            [FromForm] IFormFile? file,
            [FromQuery] string imgPath)
        {
            if (file == null)
            {
                return Ok(null);
            }

            using var ftpClient = new FtpClient("ftp://localhost", "zcxb", "123456");
            try
            {
                ftpClient.Connect();
                var uploadPath = Path.Combine("otona_book", imgPath);
                using var fStream = file.OpenReadStream();
                ftpClient.UploadStream(fStream, uploadPath, FtpRemoteExists.Overwrite, true);
                return new { upload_path = uploadPath };
            }
            finally
            {
                ftpClient.Disconnect();
            }
        }
    }
}
