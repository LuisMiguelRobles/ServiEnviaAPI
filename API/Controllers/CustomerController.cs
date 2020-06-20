namespace API.Controllers
{
    using Application.Commands.Customer;
    using Application.Queries.Customer;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class CustomerController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> List()
        {
            return await Mediator.Send(new List.Query());
        }


        [HttpGet("{document}")]
        public async Task<ActionResult<Customer>> Search(string document)
        {
            return await Mediator.Send(new Search.Query { Document = document });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await Mediator.Send(command);
        }
    }
}
