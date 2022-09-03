using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TvShowAPI.Data;
using TvShowAPI.Models;

namespace TvShowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public UserController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        //Get Actio to login , returns token
        [HttpGet("[Action]")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _dbContext.Users.FindAsync(username, password);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                string token = CreateToken();
                Sessao sessao = new Sessao();
                sessao.UserId = user.ID;
                sessao.Token = token;
                user.Token = token;
                sessao.LoginDate = DateTime.Now;

                await _dbContext.Sessoes.AddAsync(sessao);
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return Ok(token);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddFavSerie(int userid, int serieid)
        {
            if (userid != 0 && serieid != 0)
            {
                SerieUser serieuser = new SerieUser();
                serieuser.SerieID = serieid;
                serieuser.UserID = userid;

                await _dbContext.SerieUser.AddAsync(serieuser);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }

            else
            {
                return NotFound();
            }
        }

        [HttpPost("[action]")]
        public IActionResult RemoveFavSerie([FromForm] int userid, [FromForm] int serieid)
        {
            if (userid != 0 && serieid != 0)
            {
                SerieUser serieuser = new SerieUser();
                serieuser.SerieID = serieid;
                serieuser.UserID = userid;

                var hlp = _dbContext.SerieUser.Find(serieuser);
                if (hlp == null)
                {
                    return NotFound();
                }
                else
                {
                    _dbContext.SerieUser.Remove(hlp);
                    _dbContext.SaveChangesAsync();
                    return  StatusCode(StatusCodes.Status201Created);
                }

            }

            else
            {
                return NotFound();
            }


        }

        private string CreateToken()
        {
            string token = "";
            token = Guid.NewGuid().ToString();
            return token;
        }


    }
}
