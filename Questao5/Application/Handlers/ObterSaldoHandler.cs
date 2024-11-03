using MediatR;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.QueryStore.Interface;
using Questao5.Utils;

namespace Questao5.Application.Handlers
{
    public class ObterSaldoHandler : IRequestHandler<ObterSaldoQuery, ObterSaldoResponse>
    {
        private readonly IQueryStore _queryStore;

        public ObterSaldoHandler(IQueryStore queryStore)
        {
            _queryStore = queryStore;
        }

        public async Task<ObterSaldoResponse> Handle(ObterSaldoQuery query, CancellationToken cancellationToken)
        {
            var request = query.Request;

            // Validações de negócio
            var conta = await _queryStore.GetContaCorrente(request.IdContaCorrente);
            if (conta == null || !conta.Ativo)
            {
                throw new BusinessException("Conta inválida ou inativa.");
            }

            // Calcular saldo
            var saldo = await _queryStore.CalcularSaldo(request.IdContaCorrente);

            return new ObterSaldoResponse
            {
                Numero = conta.Numero,
                Nome = conta.Nome,
                DataConsulta = DateTime.UtcNow,
                SaldoAtual = saldo
            };
        }
    }

}
