using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Order
{
    using Domain;
    using MediatR;
    using Persistence;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    public class List
    {
        public class Query : IRequest<List<Order>> { }

        public class Handler : IRequestHandler<Query, List<Order>>
        {

            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Order>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders.ToListAsync(cancellationToken);

                return orders;
            }
        }
    }
}
