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

                    var california = context.States.Find("CA");
                    if (california == null)
                    {
                        context.States.Add(new StateModel("CA", "California", ""));
                        context.SaveChanges();
                        Log.Debug("California was created");
                    }
                    else
                    {
                        Log.Debug("California already exists");
                    }

                    var florida = context.States.Find("FL");
                    if (florida == null)
                    {
                        context.States.Add(new StateModel("FL", "Florida", ""));
                        context.SaveChanges();
                        Log.Debug("Florida was created");
                    }
                    else
                    {
                        Log.Debug("Florida already exists");
                    }

                    var angels = context.Cities.Find("1840020491");
                    if (angels == null)
                    {
                        context.Cities.Add(new CityModel("1840020491", "Los Angeles", "CA"));
                        context.SaveChanges();
                        Log.Debug("Los Angeles was created");
                    }
                    else
                    {
                        Log.Debug("Los Angeles already exists");
                    }

                    var sanFrancisco = context.Cities.Find("1840021543");
                    if (sanFrancisco == null)
                    {
                        context.Cities.Add(new CityModel("1840021543", "San Francisco", "CA"));
                        context.SaveChanges();
                        Log.Debug("San Francisco was created");
                    }
                    else
                    {
                        Log.Debug("San Francisco already exists");
                    }

                    var miami = context.Cities.Find("1840015149");
                    if (miami == null)
                    {
                        context.Cities.Add(new CityModel("1840015149", "Miami", "FL"));
                        context.SaveChanges();
                        Log.Debug("Miami was created");
                    }
                    else
                    {
                        Log.Debug("Miami already exists");
                    }

                    var id = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D7");
                    var userId = new Guid("545DE66E-19AC-47D2-57F6-08D8715337D9");
                    var point = new Point(34.050232, -118.249741);
                    var userLocation = context.ProfileLocations.Find(id);
                    if (userLocation == null)
                    {
                        context.ProfileLocations.Add(new ProfileLocationModel(id, userId, point, "050024", "1840021543"));
                        context.SaveChanges();
                        Log.Debug("Miami was created");
                    }
                    else
                    {
                        Log.Debug("Miami already exists");
                    }
                }
            }
        }
    }
}