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
    public class UpdateProfileLocationCommandHandler
    {
        public class Command : IRequest<ProfileLocationModel>
        {
            public Guid UserId { get; set; }
            public double Latitud { get; set; }
            public double Longitud { get; set; }
            public string ZipCode { get; set; }
            public string CityId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }

        public class Handler : IRequestHandler<Command, ProfileLocationModel>
        {
            private GeoDbContext _geoDbContext;

            public Handler(GeoDbContext geoDbContext)
            {
                _geoDbContext = geoDbContext;
            }

            public async Task<ProfileLocationModel> Handle(
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
                        throw new AppException($"The UserId {UserId} doesn't have a Profile Location");
                    }

                    existingProfileLocation.Latitud = request.Latitud;
                    existingProfileLocation.Longitud = request.Longitud;
                    existingProfileLocation.ZipCode = request.ZipCode;
                    existingProfileLocation.CityId = request.CityId;

                    _geoDbContext.ProfileLocations.Update(existingProfileLocation);
                    await _geoDbContext.SaveChangesAsync();

                    return existingProfileLocation;
                }
                catch (Exception e)
                {
                    throw new AppException(e.Message);
                }
            }
        }
    }
}
