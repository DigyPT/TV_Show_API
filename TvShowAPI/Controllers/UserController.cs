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
    public class UserController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public UserController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Metodo Post default para adicionar utilizadores
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {

                user.Password = CryptorEngine.Encrypt(user.Password);

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        //Metodo Get Action para realizar login , returns token
        [HttpGet("[Action]")]
        public async Task<IActionResult> Login(string username, string password)
        {
            //User usr = new User();
            //usr.UserName = username;
            //usr.Password = password;

            var userlst =  _dbContext.Users.Where(a => a.UserName == username).ToList();
            //_dbContext.Dispose();
            if (userlst.Count == 0)
            {
                return NotFound();
            }
            else
            {
                if(CryptorEngine.Decrypt(userlst[0].Password)==password)
                {
                    string token = CreateToken();
                    Sessao sessao = new Sessao();
                    sessao.UserId = userlst[0].ID;
                    sessao.Token = token;
                    sessao.LoginDate = DateTime.Now;
                    await _dbContext.Sessoes.AddAsync(sessao);
                    await _dbContext.SaveChangesAsync();
                    return Ok(token);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
               
            }
        }

        //Metodo Post Action para inserir séries favoritas
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFavSerie(int userid, int serieid,string token)
        {
            var sessao = _dbContext.Sessoes.Where(a => a.Token == token);
            if (sessao == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
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
        }

        //Metodo Get Action para obter detalhes do utilizador
        [HttpGet("[action]")]
        public IActionResult GetUserDetails(int? pageNumber, int? pageSize, string username, string token)
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

                var userDetails = _dbContext.Users.Where(a => a.UserName == username).Include(a => a.SerieUsers).ThenInclude(b => b.Serie);
                //var actors = _dbContext.Actors.Where(a => a.ID == id).Include(a => a.ActorSeries).ThenInclude(a => a.Serie);
                return Ok(userDetails.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
        }

        //Metodo Post Action para remover uma serie dos favoritos
        [HttpPost("[action]")]
        public IActionResult RemoveFavSerie(int userid, int serieid,string token)
        {
            var sessao = _dbContext.Sessoes.Where(a => a.Token == token);
            if (sessao == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                if (userid != 0 && serieid != 0)
                {
                    SerieUser serieUser = new SerieUser();
                    serieUser.SerieID = serieid;
                    serieUser.UserID = userid;

                    var hlp = _dbContext.SerieUser.AsNoTracking().Where(a => a.UserID == userid).Where(a => a.SerieID == serieid);

                    if (hlp == null)
                    {
                        return NotFound();
                    }
                    else
                    {

                        _dbContext.SerieUser.Remove(serieUser);
                        _dbContext.SaveChanges();

                        return StatusCode(StatusCodes.Status201Created);
                    }

                }

                else
                {
                    return NotFound();
                }
            }


        }

        //Funcao para criar token aleatórios
        private string CreateToken()
        {
            string token = "";
            token = Guid.NewGuid().ToString();
            return token;
        }


    }
}
