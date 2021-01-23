using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseList.API.ViewModels;

namespace PurchaseList.API.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected ApiBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> SendCommand(IRequest<ResultViewModel> request)
        {
            var result = await _mediator.Send(request).ConfigureAwait(false);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}