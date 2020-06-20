namespace Application.Queries.Customer
{
    using Domain;
    using Errors;
    using MediatR;
    using Persistence;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;

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

                var customers = new List<Customer>
                {
                    new Customer
                    {
                        Document = "123",
                        FirstName = "Uno",
                        LastName = "Uno",
                        Email = "test@test.test"
                    },
                    new Customer
                    {
                        Document = "132",
                        FirstName = "Dos",
                        LastName = "Uno",
                        Email = "test@test.test"
                    }
                };

                //var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Document == request.Document, cancellationToken: cancellationToken);
                var customer =  customers.FirstOrDefault(x => x.Document == request.Document);

                if (customer == null)
                    throw new RestException(HttpStatusCode.NotFound, new { customer = "Not found" });

                return customer;
            }
        }
    }
}
