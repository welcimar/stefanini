using NSubstitute;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Database.CommandStore.Interface;
using Xunit;

namespace Questao5.TesteUnitario
{
    public class MovimentarContaHandlerTests
    {
        private readonly ICommandStore _commandStore;
        private readonly MovimentarContaHandler _handler;

        public MovimentarContaHandlerTests()
        {
            _commandStore = Substitute.For<ICommandStore>();
            _handler = new MovimentarContaHandler(_commandStore);
        }

        [Fact]
        public async Task Handle_WhenIdempotent_ShouldReturnPreviousResult()
        {
            var request = new MovimentarContaRequest
            {
                IdContaCorrente = "123",
                Valor = 100,
                TipoMovimento = "C",
                ChaveIdempotencia = "key123"
            };
            _commandStore.CheckIdempotency("key123").Returns("previous-id");

            var result = await _handler.Handle(new MovimentarContaCommand(request), CancellationToken.None);

            Assert.Equal("previous-id", result.IdMovimento);
        }
    }

}
