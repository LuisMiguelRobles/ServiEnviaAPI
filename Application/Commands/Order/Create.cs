using System.Linq;
using System.Net;
using Application.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Order
{
    using FluentValidation;
    using MediatR;
    using Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string SenderDocument { get; set; }
            public string ReceiverDocument { get; set; }
            public string From { get; set; }
            public string Destination { get; set; }
            public decimal Weight { get; set; }
            public decimal Price { get; set; }
            public State State { get; set; }
            public Guid CustomerId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.SenderDocument).NotEmpty();
                RuleFor(x => x.ReceiverDocument).NotEmpty();
                //RuleFor(x => x.From).NotEmpty();
                RuleFor(x => x.Destination).NotEmpty();
                RuleFor(x => x.Weight).NotEmpty();
                RuleFor(x => x.Price).NotEmpty();
                RuleFor(x => x.CustomerId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = new Order
                {
                    Id = request.Id,
                    SenderDocument = request.SenderDocument,
                    ReceiverDocument = request.ReceiverDocument,
                    From = request.From,
                    Destination = request.Destination,
                    Weight = request.Weight,
                    Price = request.Price,
                    State = State.Pending,
                    CustomerId = request.CustomerId
                };

                _context.Orders.Add(order);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("problem saving changes");
            }
        }
    }
}
