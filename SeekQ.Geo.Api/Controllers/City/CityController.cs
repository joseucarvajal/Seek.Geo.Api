using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeekQ.Geo.Api.Application.City.Queries;
using SeekQ.Geo.Api.Application.City.ViewModel;
using Swashbuckle.AspNetCore.Annotations;

namespace SeekQ.Geo.Api.Controllers.City
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly IMediator _mediator;

        public CityController(
            IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/v1/city
        [HttpGet ("{stateId}")]
        [SwaggerOperation(Summary = "get all available cities filtered by stateId")]
        public async Task<IEnumerable<CityViewModel>> GetAllCities(
            [SwaggerParameter(Description = "stateId is a String Type")]
            [FromRoute]
            string stateId
        )
        {
            return await _mediator.Send(new GetAllCitiesQueryHandler.Query(stateId));
        }
    }
}