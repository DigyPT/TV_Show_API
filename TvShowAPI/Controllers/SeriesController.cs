using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TvShowAPI.Data;
using TvShowAPI.Models;

namespace TvShowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public SeriesController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Metodo post default para addicionar series
        [HttpPost]
        public async Task<IActionResult> Post(Serie serie)
        {
            try
            {
                await _dbContext.Series.AddAsync(serie);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return NotFound("Erro");
            }

        }

        //Metodo get default para receber todas as series
        [HttpGet]
        public IActionResult GetSeries(int? pageNumber, int? pageSize,string token)
        {
            var sessao = _dbContext.Sessoes.Where(a => a.Token == token);
            if (sessao == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                int currentPageNumber = pageNumber ?? 1;
                int currentPageSize = pageSize ?? 10;

                var series = _dbContext.Series;
                return Ok(series.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }

        //Metodo get action , para receber mais detalhes de uma serie
        [HttpGet("[action]")]
        public IActionResult MoreDetails(int serieID, int? pageNumber, int? pageSize, string token)
        {
            var sessao = _dbContext.Sessoes.Where(a => a.Token == token);
            if (sessao == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                int currentPageNumber = pageNumber ?? 1;
                int currentPageSize = pageSize ?? 10;
                var serieEpisode = _dbContext.Series.Where(a => a.ID == serieID).Include(a => a.Episodes).Include(a => a.GenreSeries).ThenInclude(b => b.Genre).Include(a => a.ActorSeries).ThenInclude(c => c.Actor);
                return Ok(serieEpisode.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }

        //Metodo get action , para receber todos os actores de uma serie
        [HttpGet("[action]")]
        public IActionResult SeriesActors(int serieID, int? pageNumber, int? pageSize, string token)
        {
            var sessao = _dbContext.Sessoes.Where(a => a.Token == token);
            if (sessao == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                int currentPageNumber = pageNumber ?? 1;
                int currentPageSize = pageSize ?? 10;

                //Verficação

                var serieActors = _dbContext.Series.Where(a => a.ID == serieID).Include(a => a.ActorSeries).ThenInclude(a => a.Actor);
                return Ok(serieActors.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }

        //Metodo get action , para receber series de determinado género
        [HttpGet("[action]")]
        public IActionResult SerieGenre(string genre, int? pageNumber, int? pageSize,string token)
        {
            var sessao = _dbContext.Sessoes.Where(a => a.Token == token);
            if (sessao == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                int currentPageNumber = pageNumber ?? 1;
                int currentPageSize = pageSize ?? 10;
                //var teste = _dbContext.Series.FromSqlRaw("select * from AtorSerie");
                var serieEpisode = _dbContext.Genres.Where(a => a.GenreName == genre).Include(a => a.GenreSeries).ThenInclude(b => b.Serie);
                return Ok(serieEpisode.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }
    }
}
