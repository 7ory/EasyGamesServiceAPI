using Dapper;
using EasyGames.Context;
using EasyGames.Models;
using System.Data;
using System.Text;
namespace EasyGames.BusinessLogic
{
    public class DashboardLogic
    {

        private readonly IConfiguration _configuration;
        private readonly DapperContext _context;

        public DashboardLogic(IConfiguration configuration, DapperContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<List<Client>?> GetClients(string? filter)
        {
            var query = new StringBuilder();
            query.Append(" SELECT *");
            query.Append(" FROM Client");
            if (!String.IsNullOrWhiteSpace(filter))
            {
                query.Append($" WHERE {filter}");
            }

            using (var connection = _context.CreateConnection())
            {
                var clients = await connection.QueryAsync<Client>(query.ToString());

                return clients.AsList();
            }

        }

        public async Task<List<TransactionType>?> GetTransactionType(string? filter)
        {
            var query = new StringBuilder();
            query.Append(" SELECT *");
            query.Append(" FROM TransactionType");
            if (!String.IsNullOrWhiteSpace(filter))
            {
                query.Append($" WHERE {filter}");
            }

            using (var connection = _context.CreateConnection())
            {
                var transactionTypes = await connection.QueryAsync<TransactionType>(query.ToString());

                return transactionTypes.AsList();
            }

        }

        public async Task<List<Transaction>?> GetTransaction(string? filter)
        {
            var query = new StringBuilder();
            query.Append(" SELECT *");
            query.Append(" FROM [Transaction]");
            if (!String.IsNullOrWhiteSpace(filter))
            {
                query.Append($" WHERE {filter}");
            }

            using (var connection = _context.CreateConnection())
            {
                var transactions = await connection.QueryAsync<Transaction>(query.ToString());

                return transactions.AsList();
            }

        }

        public async Task<bool> UpsertTransaction(TransactionDTO transaction)
        {
            using (var connection = _context.CreateConnection())
            {

                var result = await connection.ExecuteAsync("UpsertTransaction", transaction, commandType: CommandType.StoredProcedure);
                return true;
            }

        }

    }
}
