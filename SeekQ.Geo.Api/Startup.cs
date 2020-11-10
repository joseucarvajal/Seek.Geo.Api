using App.Common.DependencyInjection;
using App.Common.Middlewares;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeekQ.Geo.Api.Application.City.Queries;
using SeekQ.Geo.Api.Application.Profile.Commands;
using SeekQ.Geo.Api.Application.Profile.Queries;
using SeekQ.Geo.Api.Application.State.Queries;
using SeekQ.Geo.Api.Data;

namespace SeekQ.Geo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddControllersWithViews()
                    .AddFluentValidation(cfg =>
                    {
                        cfg.RegisterValidatorsFromAssemblyContaining<GetAllStatesQueryHandler>();
                        cfg.RegisterValidatorsFromAssemblyContaining<GetAllCitiesQueryHandler>();
                        cfg.RegisterValidatorsFromAssemblyContaining<GetProfileLocationQueryHandler>();
                        cfg.RegisterValidatorsFromAssemblyContaining<CreateProfileLocationCommandHandler>();
                        cfg.RegisterValidatorsFromAssemblyContaining<UpdateProfileLocationCommandHandler>();
                        cfg.RegisterValidatorsFromAssemblyContaining<DeleteProfileLocationCommandHandler>();

                        cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    });

            services.AddCustomMSSQLDbContext<GeoDbContext>(Configuration)
                    .AddMediatR(typeof(GetAllStatesQueryHandler).Assembly);

            services.AddCustomMSSQLDbContext<GeoDbContext>(Configuration)
                    .AddMediatR(typeof(GetAllCitiesQueryHandler).Assembly);

            services.AddCustomMSSQLDbContext<GeoDbContext>(Configuration)
                    .AddMediatR(typeof(CreateProfileLocationCommandHandler).Assembly);

            services.AddCustomMSSQLDbContext<GeoDbContext>(Configuration)
                    .AddMediatR(typeof(UpdateProfileLocationCommandHandler).Assembly);

            services.AddCustomMSSQLDbContext<GeoDbContext>(Configuration)
                    .AddMediatR(typeof(DeleteProfileLocationCommandHandler).Assembly);

            services.AddSwaggerGen(config => {
                config.CustomSchemaIds(x => x.FullName);
                config.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
                c.RoutePrefix = "swagger"; //Swagger at the  project root URL
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
