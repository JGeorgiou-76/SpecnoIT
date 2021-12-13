using API.Extensions;
using API.Helpers;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adding Extention Methods to clean up the startup Class
            //Adding our created extention service for the Token created for users and the Connection String to our Database
            services.AddApplicationServices(_config);

            services.AddControllers();

            //service.addCors();

            //Adding our created Identity service to Authenticate the Users
            services.AddIdentityServices(_config);
           
            services.AddSwaggerGen(c =>
            {
                const string name = "OAuth2";

                c.AddSecurityDefinition(name, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementOperationFilter>(name);

                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "SpencoIT API", 
                    Version = "v1",
                    Description = "Specno Technical Assessment (Junior Back End) - Makeshift Reddit API",
                    Contact = new OpenApiContact
                    {
                        Name = "Jason Georgiou",
                        Email = "georgiouj76@gmail.com"
                    }
                });
            });    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors();

            app.UseAuthentication();           
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
