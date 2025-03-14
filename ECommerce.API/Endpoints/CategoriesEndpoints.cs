using ECommerce.API.Extensions;
using ECommerce.Application.Contracts.Requests;
using ECommerce.Application.Services;
using ECommerce.Core.Enums;
using ECommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace ECommerce.API.Endpoints
{
    public static class CategoriesEndponts
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("category", AddCategory).RequirePermissions(Permission.Create);
            app.MapGet("categories", GetAllCategories).RequirePermissions(Permission.Read);
            app.MapPut("category", UpdateCategory).RequirePermissions(Permission.Update);
            app.MapDelete("category/{categoryName}", DeleteCategory).RequirePermissions(Permission.Delete);
            return app;
        }
        public static async Task<IResult> DeleteCategory(string categoryName, CategoryService service)
        {
            await service.DeleteCategory(categoryName);
            return Results.Ok();
        }
        public static async Task<IResult> UpdateCategory(CategoryRequest request, CategoryService service)
        {
            await service.UpdateCategory(request.Name, request.Description);
            return Results.Ok();
        }

        public static async Task<IResult> AddCategory(CategoryRequest request,
            CategoryService categoryService)
        {
            await categoryService.AddCategory(request.Name, request.Description);
            return Results.Ok();
        }
        public static async Task<IResult> GetAllCategories(CategoryService categoryService)
        {
            var categories = await categoryService.GetAllCategories();

            return Results.Ok(categories);
        }
    }
}
