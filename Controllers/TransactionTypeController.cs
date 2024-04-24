using EasyGames.BusinessLogic;
using EasyGames.Context;
using EasyGames.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyGames.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DapperContext _context;

        public TransactionTypeController(IConfiguration configuration, DapperContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactionType([FromQuery] GetParameters prmtrs)
        {

            var logic = new DashboardLogic(_configuration, _context);

            var results = await logic.GetTransactionType(prmtrs.filter);

            if (results == null)
            {
                return BadRequest();
            }

            return Ok(results);

        }
    }
}
