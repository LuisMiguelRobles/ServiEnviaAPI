namespace Application.Commands.Order
{
    using FluentValidation;
    using MediatR;
    using Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Application.Errors;
    using System.Net;

    public class Update
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public State State { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.State).NotEmpty();
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
                var order = await _context.Orders.FindAsync(request.Id);

                if(order == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Order = "Not Found" });

                order.State = request.State;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("problem saving changes");
            }
        }
    }
}
