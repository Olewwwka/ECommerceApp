using ECommerce.Application.Contracts.Requests;
using ECommerce.Application.Services;
using ECommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace ECommerce.API.Endpoints
{
    public static class CategoriesEndponts
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("CreateCategory", AddCategory);
            app.MapGet("GetCategories", GetAllCategories);
            app.MapPut("UpdateCategory", UpdateCategory);
            app.MapDelete("DeleteCategory/{categoryName}", DeleteCategory);
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
