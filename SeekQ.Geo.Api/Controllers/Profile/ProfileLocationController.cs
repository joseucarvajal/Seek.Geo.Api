using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeekQ.Geo.Api.Application.Location.ViewModel;
using SeekQ.Geo.Api.Application.Profile.Commands;
using SeekQ.Geo.Api.Application.Profile.Queries;
using SeekQ.Geo.Api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SeekQ.Geo.Api.Controllers.Profile
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfileLocationController : Controller
    {
        private readonly IMediator _mediator;
        public ProfileLocationController(
            IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/v1/profilelocation
        [HttpGet("{userId}")]
        [SwaggerOperation(Summary = "get user location information")]
        public async Task<IEnumerable<ProfileLocationViewModel>> GetUserLocation(
            [SwaggerParameter(Description = "userId is a Guid Type")]
            [FromRoute] Guid userId
        )
        {
            return await _mediator.Send(new GetProfileLocationQueryHandler.Query(userId));
        }

        // POST api/v1/profilelocation
        [HttpPost]
        [SwaggerOperation(Summary = "create geo location for an user")]
        [SwaggerResponse((int)HttpStatusCode.OK, "geo location created succesfully")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        public async Task<ActionResult<ProfileLocationModel>> CreateUserLocation(
            [FromBody] CreateProfileLocationCommandHandler.Command command
        )
        {
            return await _mediator.Send(command);
        }

        // PUT api/v1/profilelocation
        [HttpPut]
        [SwaggerOperation(Summary = "update geo location for an user")]
        [SwaggerResponse((int)HttpStatusCode.OK, "geo location updated succesfully")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        public async Task<ActionResult<ProfileLocationModel>> UpdateUserLocation(
            [FromBody] UpdateProfileLocationCommandHandler.Command command
        )
        {
            return await _mediator.Send(command);
        }

        // DELETE api/v1/profilelocation
        [HttpDelete("{userId}")]
        [SwaggerOperation(Summary = "delete geo location for an user")]
        public async Task<bool> DeleteUserLocation(
            [SwaggerParameter(Description = "userId is a Guid Type")]
            [FromRoute] Guid userId
        )
        {
            return await _mediator.Send(new DeleteProfileLocationCommandHandler.Command(userId));
        }
    }
}