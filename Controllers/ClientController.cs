using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyGames.Context;
using EasyGames.Models;
using EasyGames.BusinessLogic;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EasyGames.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DapperContext _context;

        public ClientController(IConfiguration configuration, DapperContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetClients([FromQuery] GetParameters prmtrs)
        {

            var logic = new DashboardLogic(_configuration, _context);

            var results = await logic.GetClients(prmtrs.filter);

            if (results == null)
            {
                return BadRequest();
            }

            return Ok(results);

        }


    }
}
