using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Interface;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class QueryStore : IQueryStore
    {
        private readonly IDbConnection _dbConnection;

        public QueryStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ContaCorrente> GetContaCorrente(string idContaCorrente)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(
                "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente",
                new { IdContaCorrente = idContaCorrente });
        }

        public async Task<decimal> CalcularSaldo(string idContaCorrente)
        {
            var creditos = await _dbConnection.ExecuteScalarAsync<decimal>(
                "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'C'",
                new { IdContaCorrente = idContaCorrente });
            var debitos = await _dbConnection.ExecuteScalarAsync<decimal>(
                "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'D'",
                new { IdContaCorrente = idContaCorrente });
            return creditos - debitos;
        }
    }

}
