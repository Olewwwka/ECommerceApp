using ECommerce.API.Contracts.Requests;
using ECommerce.Application.Services;

namespace ECommerce.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            app.MapGet("getall", GetAllUsers);
            return app;
        }

        public static async Task<IResult> Register(RegisterUserRequest request,
            UserService userService)
        {
            await userService.Register(request.Login, request.Email, request.Password, request.FirstName, request.LastName);

            return Results.Ok();
        }

        public static async Task<IResult> Login(LoginUserRequest request, UserService userService)
        {
            var token = await userService.Login(request.Email, request.Password);

            return Results.Ok(token);
        }

        public static async Task<IResult> GetAllUsers(UserService userService)
        {
            var users = await userService.GetAllUsers();

            return Results.Ok(users);
        }
    }
}
