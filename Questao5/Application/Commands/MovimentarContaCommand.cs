using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands
{
    public class MovimentarContaCommand : IRequest<MovimentarContaResponse>
    {
        public MovimentarContaRequest Request { get; set; }

        public MovimentarContaCommand(MovimentarContaRequest request)
        {
            Request = request;
        }
    }
}
