using System;
using System.Threading;
using System.Threading.Tasks;
using App.Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SeekQ.Geo.Api.Data;
using SeekQ.Geo.Api.Models;

namespace SeekQ.Geo.Api.Application.Profile.Commands
{
    public class DeleteProfileLocationCommandHandler
    {
        public class Command : IRequest<bool>
        {
            public Command(Guid userId)
            {
                UserId = userId;
            }

            public Guid UserId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private GeoDbContext _geoDbContext;

            public Handler(GeoDbContext geoDbContext)
            {
                _geoDbContext = geoDbContext;
            }

            public async Task<bool> Handle(
                Command request,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    Guid UserId = request.UserId;

                    var existingProfileLocation = await _geoDbContext.ProfileLocations
                                                    .AsNoTracking()
                                                    .SingleOrDefaultAsync(profile => profile.UserId == UserId);

                    if (existingProfileLocation == null)
                    {
                        return false;
                    }

                    _geoDbContext.ProfileLocations.Remove(existingProfileLocation);
                    await _geoDbContext.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    throw new AppException(e.Message);
                }
            }
        }
    }
}
