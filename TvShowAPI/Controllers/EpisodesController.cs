using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TvShowAPI.Data;
using TvShowAPI.Models;
using System.Linq;

namespace TvShowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public EpisodesController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Metodo Post default para adicionar episódios
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Episode episode)
        {
            try
            {
                await _dbContext.Episodes.AddAsync(episode);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

        }

        //Metodo Get default para receber todos os episódios
        [HttpGet]
        public IActionResult GetEpisodes(int? pageNumber, int? pageSize, string token)
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

                var episodes = _dbContext.Episodes;
                return Ok(episodes.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }
    }
}
