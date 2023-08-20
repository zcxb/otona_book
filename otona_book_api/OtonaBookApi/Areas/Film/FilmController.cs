using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtonaBookApi.Common;
using OtonaBookApi.Controllers;
using Models = OtonaBookApi.DataAccess.Models;

namespace OtonaBookApi.Areas.Film
{
    [Route("film")]
    public class FilmController : AreaControllerBase
    {
        class QueryTuple
        {
            public Models.Film Film { get; set; }
            public Models.FilmActress FilmActress { get; set; }
        }

        [HttpPost("query-list")]
        public async Task<ResponseResult<object>> GetFilmList([FromBody] QueryFilmListRequest req)
        {
            var query = from filmActress in DbContext.FilmActress
                        join film in DbContext.Film
                        on filmActress.FilmId equals film.Id into joined
                        from film in joined.DefaultIfEmpty()
                        select new QueryTuple { Film = film, FilmActress = filmActress };

            var predicate = PredicateBuilder.New<QueryTuple>(true);

            if (!string.IsNullOrWhiteSpace(req.Title))
            {
                predicate.And(o => o.Film.Title.Contains(req.Title));
            }
            if (req.ActressIds != null && req.ActressIds.Length > 0)
            {
                predicate.And(o => req.ActressIds.Contains(o.FilmActress.ActressId));
            }

            return await query.AsExpandable()
                .Where(predicate)
                .OrderByDescending(o => o.Film.PublishedAt)
                .Select(o => new QueryFilmListResponse
                {
                    Bango = o.Film.Bango,
                    Title = o.Film.Title,
                    CoverImages = o.Film.CoverImages,
                })
                .Distinct()
                .Skip(req.PageSize * (req.PageNo - 1))
                .Take(req.PageSize)
                .ToListAsync();
        }
    }
}
