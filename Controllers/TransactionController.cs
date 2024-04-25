using EasyGames.BusinessLogic;
using EasyGames.Context;
using EasyGames.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EasyGames.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DapperContext _context;

        public TransactionController(IConfiguration configuration, DapperContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetTransaction([FromQuery] GetParameters prmtrs)
        {

            var logic = new DashboardLogic(_configuration, _context);

            var results = await logic.GetTransaction(prmtrs.filter);

            if (results == null)
            {
                return BadRequest();
            }

            return Ok(results);

        }

        [HttpPost]
        public async Task<ActionResult> PostTransaction(TransactionDTO transaction)
        {
            var logic = new DashboardLogic(_configuration, _context);
            var transactionParams = JsonConvert.DeserializeObject<TransactionDTO>(JsonConvert.SerializeObject(transaction));
            transactionParams!.Delete = false;
            var results = await logic.UpsertTransaction(transactionParams);

            return Ok(results);

        }

        [HttpPut("{transactionID}")]
        public async Task<ActionResult> PutTransaction(int transactionID, Transaction transaction)
        {
            var logic = new DashboardLogic(_configuration, _context);

            

            var transactionParams = JsonConvert.DeserializeObject<TransactionDTO>(JsonConvert.SerializeObject(transaction));
            transactionParams!.Delete = false;
            transactionParams.TransactionID = transactionID;
            var results = await logic.UpsertTransaction(transactionParams);

            if (!results)
            {
                return BadRequest("Something went wrong, Please contact your administrator");
            }

            return Ok(results);

        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteTransaction(int id)
        //{
        //    var logic = new DashboardLogic(_configuration, _context);

        //    var transactionS = await logic.GetTransaction($"TransactionID = {id}");

        //    if (transactionS == null || !transactionS.Any())
        //    {
        //        return BadRequest("Something went wrong, Couldn't find and delete the transaction being parsed");
        //    }

        //    var transaction = transactionS.First();

        //    var transactionParams = JsonConvert.DeserializeObject<TransactionDTO>(JsonConvert.SerializeObject(transaction));
        //    transactionParams!.Delete = true;
        //    var results = await logic.UpsertTransaction(transactionParams);

        //    if (!results)
        //    {
        //        return BadRequest("Something went wrong, Please contact your administrator");
        //    }
        //    return Ok(results);
        //}
    }
}
