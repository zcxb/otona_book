using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtonaBookApi.Common;
using OtonaBookApi.Controllers;
using OtonaBookApi.DataAccess;
using TModel = OtonaBookApi.DataAccess.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OtonaBookApi.Areas.Actress
{
    [Route("actress")]
    public class ActressController : AreaControllerBase
    {
        private readonly OtonaBookContext _dbContext;

        public ActressController(ILogger<ActressController> logger, OtonaBookContext dbContext)
            : base(logger)
        {
            _dbContext = dbContext;
        }

        [HttpPost("get-actress-list")]
        public async Task<ResponseResult<List<ActressResponse>>> GetActressList()
        {
            var list = await _dbContext.Actress.Select(a => new ActressResponse { Name = a.Name, Avatar = a.Avatar, Info = a.Info }).ToListAsync();
            return list;
        }

        [HttpPost("set-actress")]
        public async Task<ResponseResult<int>> SetActress([FromBody] SetActressRequest req)
        {
            var new_actress = await _dbContext.Actress.AddAsync(new TModel.Actress { Name = req.Name, Avatar = req.Avatar, Info = req.Info });
            await _dbContext.SaveChangesAsync();
            return new_actress.Entity.Id;
        }
    }
}

