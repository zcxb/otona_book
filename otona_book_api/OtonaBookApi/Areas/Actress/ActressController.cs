using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtonaBookApi.Common;
using OtonaBookApi.Controllers;
using OtonaBookApi.DataAccess;
using Models = OtonaBookApi.DataAccess.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OtonaBookApi.Areas.Actress
{
    [Route("actress")]
    public class ActressController : AreaControllerBase
    {
        [HttpPost("query-list")]
        public async Task<ResponseResult<List<ActressResponse>>> GetActressList()
        {
            Logger.LogInformation("yeee");
            var list = await DbContext.Actress.Select(a => new ActressResponse { Name = a.Name, Avatar = a.Avatar, Info = a.Info }).ToListAsync();
            return list;
        }

        [HttpPost("set")]
        public async Task<ResponseResult<int>> SetActress([FromBody] SetActressRequest req)
        {
            var new_actress = await DbContext.Actress.AddAsync(new Models.Actress { Name = req.Name, Avatar = req.Avatar, Info = req.Info });
            await DbContext.SaveChangesAsync();
            return new_actress.Entity.Id;
        }
    }
}

