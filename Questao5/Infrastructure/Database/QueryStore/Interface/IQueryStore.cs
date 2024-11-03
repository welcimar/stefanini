using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Interface
{
    public interface IQueryStore
    {
        Task<ContaCorrente> GetContaCorrente(string idContaCorrente);
        Task<decimal> CalcularSaldo(string idContaCorrente);
    }
}
