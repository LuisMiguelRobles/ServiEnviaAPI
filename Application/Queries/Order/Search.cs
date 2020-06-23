namespace Application.Queries.Order
{
    using Domain;
    using Errors;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    public class Search
    {
        public class Query : IRequest<Order>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Order>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Order> Handle(Query request, CancellationToken cancellationToken)
            {

                var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (order == null)
                    throw new RestException(HttpStatusCode.NotFound, new { order = "Not found" });

                return order;
            }
        }
    }
}
