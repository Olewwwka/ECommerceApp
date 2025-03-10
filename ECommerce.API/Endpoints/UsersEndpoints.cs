using ECommerce.API.Contracts.Requests;
using ECommerce.Application.Services;
using ECommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace ECommerce.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            app.MapGet("getall", GetAllUsers);
            app.MapPost("/refresh-token", RefreshToken);
            return app;
        }

        public static async Task<IResult> RefreshToken(HttpContext context, string refreshToken)
        {
            return Results.Ok();
        }


        public static async Task<IResult> Register(RegisterUserRequest request,
            UserService userService)
        {
            await userService.Register(request.Login, request.Email, request.Password, request.FirstName, request.LastName);

            return Results.Ok();
        }

        public static async Task<IResult> Login(LoginUserRequest request, UserService userService, HttpContext context)
        {
            var (jwtToken, refreshToken) = await userService.Login(request.Email, request.Password);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(3)
            };
            var Responce = context.Response;
            Responce.Cookies.Append("jwtToken", jwtToken, cookieOptions);
            Responce.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            return Results.Ok(jwtToken);
        }

        public static async Task<IResult> GetAllUsers(UserService userService)
        {
            var users = await userService.GetAllUsers();

            return Results.Ok(users);
        }
    }
}
