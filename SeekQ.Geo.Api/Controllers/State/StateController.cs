using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeekQ.Geo.Api.Application.State.Queries;
using SeekQ.Geo.Api.Application.State.ViewModel;
using Swashbuckle.AspNetCore.Annotations;

namespace SeekQ.Geo.Api.Controllers.State
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly IMediator _mediator;

        public StateController(
            IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/v1/state
        [HttpGet]
        [SwaggerOperation(Summary = "get all available states")]
        public async Task<IEnumerable<StateViewModel>> GetAllStates()
        {
            return await _mediator.Send(new GetAllStatesQueryHandler.Query());
        }
    }
}