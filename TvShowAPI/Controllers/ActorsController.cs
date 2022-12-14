using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Threading.Tasks;
using TvShowAPI.Data;
using TvShowAPI.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TvShowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public ActorsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Método post default para adicionar Actor  
        [HttpPost]
        public async Task<IActionResult> Post(Actor actor)
        {
            try
            {
                //Falta fazer verificação de episódios

                await _dbContext.Actors.AddAsync(actor);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

        }

        //Metodo Get para obter todos os actores
        [HttpGet]
        public IActionResult GetActors(int? pageNumber, int? pageSize, string token)
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

                var actors = _dbContext.Actors;

                return Ok(actors.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }

        //Metodo Get para obter todas as séries que um ator participou
        [HttpGet("[action]")]
        public IActionResult GetAllSeries(int? pageNumber, int? pageSize, string actorName, string token)
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

                var actorid = _dbContext.Actors.Where(a => a.Name == actorName).ToList();
                int id = Convert.ToInt32(actorid[0].ID.ToString());

                var actors = _dbContext.Actors.Where(a => a.ID == id).Include(a => a.ActorSeries).ThenInclude(a => a.Serie);
                return Ok(actors.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }

    }
}
