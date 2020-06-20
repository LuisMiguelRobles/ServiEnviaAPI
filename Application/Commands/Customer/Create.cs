namespace Application.Commands.Customer
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
        public class Command: IRequest
        {
            public Guid Id { get; set; }
            public string Document { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Email { get; set; }
        }

        public class Validator: AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Document).NotEmpty();
                RuleFor(x => x.BirthDate).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
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
                var customer = new Customer
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Document = request.Document,
                    BirthDate = request.BirthDate,
                    Email = request.Email
                };
                _context.Customers.Add(customer);
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if(success) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}
