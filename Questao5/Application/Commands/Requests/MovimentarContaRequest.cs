﻿using MediatR;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContaRequest : IRequest<string>
    {
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
        public string ChaveIdempotencia { get; set; }
    }
}
