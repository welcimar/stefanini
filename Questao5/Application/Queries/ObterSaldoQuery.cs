using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries
{
    public class ObterSaldoQuery : IRequest<ObterSaldoResponse>
    {
        public ObterSaldoRequest Request { get; set; }

        public ObterSaldoQuery(ObterSaldoRequest request)
        {
            Request = request;
        }
    }
}
