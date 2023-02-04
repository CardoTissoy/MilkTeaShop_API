using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        // Get or sets the property of mediator
        public IMediator? Mediator { get; set; }

        // Gets the property for Mediatr.
        protected IMediator Mediatr => this.Mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();
    }
}
