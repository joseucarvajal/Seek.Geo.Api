using System;
using System.Threading;
using System.Threading.Tasks;
using App.Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeekQ.Geo.Api.Data;
using SeekQ.Geo.Api.Models;

namespace SeekQ.Geo.Api.Application.Profile.Commands
{
    public class CreateProfileLocationCommandHandler
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

                    if (existingProfileLocation != null)
                    {
                        throw new AppException($"The UserId {UserId} already has a Profile Location");
                    }

                    ProfileLocationModel profileLocation = new ProfileLocationModel
                    {
                        UserId = UserId,
                        Latitud = request.Latitud,
                        Longitud = request.Longitud,
                        ZipCode = request.ZipCode,
                        CityId = request.CityId
                    };

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