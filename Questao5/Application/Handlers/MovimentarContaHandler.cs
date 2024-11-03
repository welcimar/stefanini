using MediatR;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.Database.CommandStore.Interface;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaHandler : IRequestHandler<MovimentarContaCommand, MovimentarContaResponse>
    {
        private readonly ICommandStore _commandStore;

        public MovimentarContaHandler(ICommandStore commandStore)
        {
            _commandStore = commandStore;
        }

        public async Task<MovimentarContaResponse> Handle(MovimentarContaCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            // Validação de idempotência
            var idempotencyResult = await _commandStore.CheckIdempotency(request.ChaveIdempotencia);
            if (idempotencyResult != null)
            {
                return new MovimentarContaResponse { IdMovimento = idempotencyResult };
            }

            // Validações de negócio
            await _commandStore.ValidateContaCorrente(request.IdContaCorrente);
            await _commandStore.ValidateMovimento(request);

            // Persistir movimento
            var idMovimento = await _commandStore.PersistMovimento(request);

            // Registrar idempotência
            await _commandStore.SaveIdempotency(request.ChaveIdempotencia, idMovimento);

            return new MovimentarContaResponse { IdMovimento = idMovimento };
        }
    }

}
