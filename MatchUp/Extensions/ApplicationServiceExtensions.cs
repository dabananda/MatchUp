using MatchUp.Data;
using MatchUp.Interfaces;
using MatchUp.Services;
using Microsoft.EntityFrameworkCore;

namespace MatchUp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddControllers();
            services.AddOpenApi();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddCors();

            return services;
        }
    }
}
