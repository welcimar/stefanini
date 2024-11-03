using Microsoft.AspNetCore.Mvc;
using MediatR;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimentar")]
        public async Task<IActionResult> Movimentar([FromBody] MovimentarContaRequest request)
        {
            try
            {
                var response = await _mediator.Send(new MovimentarContaCommand(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        [HttpGet("saldo")]
        public async Task<IActionResult> ObterSaldo([FromQuery] ObterSaldoRequest request)
        {
            try
            {
                var response = await _mediator.Send(new ObterSaldoQuery(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }
    }

}