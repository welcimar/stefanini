using Dapper;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Interface;
using Questao5.Utils;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class CommandStore : ICommandStore
    {
        private readonly IDbConnection _dbConnection;

        public CommandStore(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<string> CheckIdempotency(string chaveIdempotencia)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<string>(
                "SELECT resultado FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia",
                new { ChaveIdempotencia = chaveIdempotencia });
        }

        public async Task ValidateContaCorrente(string idContaCorrente)
        {
            var conta = await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(
                "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente",
                new { IdContaCorrente = idContaCorrente });
            if (conta == null || !conta.Ativo)
            {
                throw new BusinessException("Conta inválida ou inativa.");
            }
        }

        public async Task ValidateMovimento(MovimentarContaRequest request)
        {
            if (request.Valor <= 0) throw new BusinessException("Valor inválido.", "INVALID_VALUE");
            if (request.TipoMovimento != "C" && request.TipoMovimento != "D") throw new BusinessException("Tipo de movimento inválido.", "INVALID_TYPE");
        }

        public async Task<string> PersistMovimento(MovimentarContaRequest request)
        {
            var idMovimento = Guid.NewGuid().ToString();
            await _dbConnection.ExecuteAsync(
                "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)",
                new { IdMovimento = idMovimento, IdContaCorrente = request.IdContaCorrente, DataMovimento = DateTime.Now.ToString("dd/MM/yyyy"), TipoMovimento = request.TipoMovimento, Valor = request.Valor });
            return idMovimento;
        }

        public async Task SaveIdempotency(string chaveIdempotencia, string resultado)
        {
            await _dbConnection.ExecuteAsync(
                "INSERT INTO idempotencia (chave_idempotencia, resultado) VALUES (@ChaveIdempotencia, @Resultado)",
                new { ChaveIdempotencia = chaveIdempotencia, Resultado = resultado });
        }
    }

}
