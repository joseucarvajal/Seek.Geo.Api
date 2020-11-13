using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using SeekQ.Geo.Api.Data;
using SeekQ.Geo.Api.Models;
using Serilog;

namespace SeekQ.Geo.Api
{
    public class SeedData
    {
        // Statics IDs for State
        public static readonly string ID_CALIFORNIA_STATE = "CA";
        public static readonly string ID_FLORIDA_STATE = "FL";

        // Statics IDs for Cities
        public static readonly string ID_LOS_ANGELES_CITY = "1840021543";
        public static readonly string ID_SAN_FRANCISCO_CITY = "1840021543";
        public static readonly string ID_MIAMI_CITY = "1840015149";

        // Statics IDs for users
        public static readonly Guid ID_USER_MOCK1 = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D7");
        public static readonly Guid ID_USER_MOCK2 = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D8");
        public static readonly Guid ID_USER_MOCK3 = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D9");

        // Statics IDs for tupla
        public static readonly Guid ID_ROW_1 = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D1");
        public static readonly Guid ID_ROW_2 = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D2");
        public static readonly Guid ID_ROW_3 = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D3");

        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();

            services.AddDbContext<GeoDbContext>(options =>
               options.UseSqlServer(connectionString));

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<GeoDbContext>();

                    context.Database.EnsureCreated();

                    var california = context.States.Find(ID_CALIFORNIA_STATE);
                    if (california == null)
                    {
                        context.States.Add(new StateModel { StateId = ID_CALIFORNIA_STATE, StateName = "California" });
                        context.SaveChanges();
                        Log.Debug("California was created");
                    }
                    else
                    {
                        Log.Debug("California already exists");
                    }

                    var florida = context.States.Find(ID_FLORIDA_STATE);
                    if (florida == null)
                    {
                        context.States.Add(new StateModel { StateId = ID_FLORIDA_STATE, StateName = "Florida" });
                        context.SaveChanges();
                        Log.Debug("Florida was created");
                    }
                    else
                    {
                        Log.Debug("Florida already exists");
                    }

                    var angels = context.Cities.Find(ID_LOS_ANGELES_CITY);
                    if (angels == null)
                    {
                        context.Cities.Add(new CityModel { CityId = ID_LOS_ANGELES_CITY, CityName = "Los Angeles", StateId = ID_CALIFORNIA_STATE });
                        context.SaveChanges();
                        Log.Debug("Los Angeles was created");
                    }
                    else
                    {
                        Log.Debug("Los Angeles already exists");
                    }

                    var sanFrancisco = context.Cities.Find(ID_SAN_FRANCISCO_CITY);
                    if (sanFrancisco == null)
                    {
                        context.Cities.Add(new CityModel { CityId = ID_SAN_FRANCISCO_CITY, CityName = "San Francisco", StateId = ID_CALIFORNIA_STATE });
                        context.SaveChanges();
                        Log.Debug("San Francisco was created");
                    }
                    else
                    {
                        Log.Debug("San Francisco already exists");
                    }

                    var miami = context.Cities.Find(ID_MIAMI_CITY);
                    if (miami == null)
                    {
                        context.Cities.Add(new CityModel { CityId = ID_MIAMI_CITY, CityName = "Miami", StateId = ID_FLORIDA_STATE });
                        context.SaveChanges();
                        Log.Debug("Miami was created");
                    }
                    else
                    {
                        Log.Debug("Miami already exists");
                    }

                    var id = ID_ROW_1;
                    var userId = ID_USER_MOCK1;
                    double latitud = 34.050232;
                    double longitud = -118.249741;
                    var userLocation = context.ProfileLocations.Find(id);
                    if (userLocation == null)
                    {
                        context.ProfileLocations.Add(
                            new ProfileLocationModel
                            {
                                Id = id,
                                UserId = userId,
                                Latitud = latitud,
                                Longitud = longitud,
                                ZipCode = "050024",
                                CityId = ID_SAN_FRANCISCO_CITY
                            });
                        context.SaveChanges();
                        Log.Debug("User Location 1 was created");
                    }
                    else
                    {
                        Log.Debug("User Location 1 already exists");
                    }

                    id = ID_ROW_2;
                    userId = ID_USER_MOCK2;
                    latitud = 37.757796;
                    longitud = -122.437479;
                    userLocation = context.ProfileLocations.Find(id);
                    if (userLocation == null)
                    {
                        context.ProfileLocations.Add(
                            new ProfileLocationModel
                            {
                                Id = id,
                                UserId = userId,
                                Latitud = latitud,
                                Longitud = longitud,
                                ZipCode = "050025",
                                CityId = ID_SAN_FRANCISCO_CITY
                            });
                        context.SaveChanges();
                        Log.Debug("User Location 2 was created");
                    }
                    else
                    {
                        Log.Debug("User Location 2 already exists");
                    }
                }
            }
        }
    }
}