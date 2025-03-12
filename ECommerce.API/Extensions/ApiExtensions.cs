using ECommerce.API.Endpoints;

namespace ECommerce.API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapUsersEndpoints();
        }

        public static void AddApiAutorization(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthorization()
        }
    }
}
