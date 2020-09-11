using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace S2SAuth.Sample.SimpleAuthService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appsettings = Configuration.Get<Appsettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = S2STokenDefaults.Authority;
                        options.Audience = S2STokenDefaults.Audience;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                        };
                        options.SecurityTokenValidators.Clear();
                        options.SecurityTokenValidators.Add(new S2SSecurityTokenValidator());
                        // for debuging
                        options.Events = new JwtBearerEvents
                        {
                            OnTokenValidated = async context =>
                            {
                                await Task.CompletedTask;
                            }
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(S2STokenDefaults.Policy, policy =>
                {
                    policy.RequireAuthenticatedUser()
                          .RequireClaim(S2STokenDefaults.ObjectId, appsettings.AllowedOids);
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
