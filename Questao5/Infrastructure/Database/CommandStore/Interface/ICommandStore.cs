using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Database.CommandStore.Interface
{
    public interface ICommandStore
    {
        Task<string> CheckIdempotency(string chaveIdempotencia);
        Task ValidateContaCorrente(string idContaCorrente);
        Task ValidateMovimento(MovimentarContaRequest request);
        Task<string> PersistMovimento(MovimentarContaRequest request);
        Task SaveIdempotency(string chaveIdempotencia, string resultado);
    }
}
