namespace API.Controllers
{
    using Application.Commands.Order;
    using Application.Queries.Order;
    using Domain;
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class OrderController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Order>>> List()
        {
            return await Mediator.Send(new List.Query());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Search(Guid id)
        {
            return await Mediator.Send(new Search.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<ActionResult<Unit>> UpdateStatus(Update.Command command)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await Mediator.Send(command);
        }
    }
}