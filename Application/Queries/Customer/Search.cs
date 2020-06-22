namespace Application.Queries.Customer
{
    using Domain;
    using Errors;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;


    public class Search
    {

        public class Query : IRequest<Customer>
        {
            public string Document { get; set; }
        }

        public class Handler : IRequestHandler<Query, Customer>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Customer> Handle(Query request, CancellationToken cancellationToken)
            {

                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Document == request.Document, cancellationToken: cancellationToken);
                
                if(customer == null)
                    throw new RestException(HttpStatusCode.NotFound, new { customer = "Not found" });

                return customer;
            }
        }
    }
}
