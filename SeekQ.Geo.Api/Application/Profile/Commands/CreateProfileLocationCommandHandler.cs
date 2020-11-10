using System;
using System.Threading;
using System.Threading.Tasks;
using App.Common.Exceptions;
using FluentValidation;
using MediatR;
using NetTopologySuite.Geometries;
using SeekQ.Geo.Api.Data;
using SeekQ.Geo.Api.Models;

namespace SeekQ.Geo.Api.Application.Profile.Commands
{
    public class CreateProfileLocationCommandHandler
    {
        public class Command : IRequest<ProfileLocationModel>
        {
            public Guid UserId { get; set; }
            public Point Location { get; set; }
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
                    Guid Id = Guid.NewGuid();
                    Guid UserId = request.UserId;
                    Point Location = request.Location;
                    string ZipCode = request.ZipCode;
                    string CityId = request.CityId;

                    var existingProfileLocation = _geoDbContext.ProfileLocations.Find(new { UserId });

                    if (existingProfileLocation == null)
                    {
                        throw new AppException($"The UserId {UserId} already has a Profile Location");
                    }

                    ProfileLocationModel profileLocation = new ProfileLocationModel(Id, UserId, Location, ZipCode, CityId);
                    _geoDbContext.ProfileLocations.Add(profileLocation);
                    await _geoDbContext.SaveChangesAsync();

                    return profileLocation;
                }
                catch (Exception e)
                {
                    throw new AppException(e.Message);
                }
            }
        }
    }
}