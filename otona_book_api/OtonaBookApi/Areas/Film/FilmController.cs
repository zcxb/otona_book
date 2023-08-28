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

        [HttpPost("save-film-item")]
        public async Task<ResponseResult<object>> SaveFilmItem([FromBody] SaveFilmItemRequest req)
        {
            throw new NotImplementedException();
            //using var transaction = DbContext.Database.BeginTransaction();
            //try
            //{
            //    var film = await DbContext.Film.Where(f => f.Bango == req.Bango).Select(f => new { Id = f.Id }).FirstOrDefaultAsync();
            //    if (film != null)
            //    {
            //        throw new BizException("film.already_exist", "影片已经存在");
            //    }

            //    var new_film = new Models.Film
            //    {
            //        Bango = req.Bango,
            //        PublishedAt = req.PublishedAt,
            //        Title = req.Title,
            //    };
            //    var new_film_entity = DbContext.Film.Add(new_film).Entity;

            //    DbContext.FilmActress.AddRange();

            //    await transaction.CommitAsync();
            //}
            //catch (Exception ex)
            //{
            //    await transaction.RollbackAsync();
            //}
        }

        [HttpPost("query-list")]
        public async Task<ResponseResult<object>> GetFilmList([FromBody] QueryFilmListRequest req)
        {
            Console.WriteLine($"pageNo: {req.PageNo}");
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
                .Select(o => new QueryFilmListResponse
                {
                    Id = o.Film.Id,
                    Bango = o.Film.Bango,
                    Title = o.Film.Title,
                    PublishedAt = o.Film.PublishedAt,
                    CoverImages = o.Film.CoverImages,
                })
                .Distinct()
                .OrderByDescending(o => o.PublishedAt)
                .Skip(req.PageSize * (req.PageNo - 1))
                .Take(req.PageSize)
                .ToListAsync();
        }
    }
}
