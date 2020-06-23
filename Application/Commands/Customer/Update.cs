namespace Application.Commands.Customer
{
    using Application.Errors;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Update
    {
        public class Command : IRequest
        {
            public string Document { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Email { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.BirthDate).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
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
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Document == request.Document, cancellationToken: cancellationToken);

                if (customer == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Company = "Not Found" });

                customer.FirstName = request.FirstName ?? customer.FirstName;
                customer.LastName = request.LastName ?? customer.LastName;
                customer.BirthDate = request.BirthDate != null ? request.BirthDate : customer.BirthDate;
                customer.Email = request.Email ?? customer.Email;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }

    }
}
