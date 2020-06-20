namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;


    [Route("api/[controller]")]
    [ApiController]
    public class BaseController
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
