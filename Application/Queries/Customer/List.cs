using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Customer
{
    using Domain;
    using MediatR;
    using Persistence;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class List
    {
        public class Query : IRequest<List<Customer>> { }

        public class Handler : IRequestHandler<Query, List<Customer>>
        {

            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Customer>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customers = await _context.Customers.ToListAsync(cancellationToken);

                return customers;
            }

        }
    }
}
