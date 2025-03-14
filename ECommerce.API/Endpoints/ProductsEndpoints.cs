using ECommerce.API.Extensions;
using ECommerce.Application.Contracts.Requests;
using ECommerce.Application.Services;
using ECommerce.Core.Enums;
using ECommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace ECommerce.API.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("product", AddProduct)
                .RequirePermissions(Permission.Create);
            app.MapGet("products", GetAllProducts)
                .RequirePermissions(Permission.Read);
            app.MapGet("product/{productId}", GetProductById)
                .RequirePermissions(Permission.Read);
            app.MapGet("products/{categoryName}", GetProductsByCategory)
                .RequirePermissions(Permission.Read);
            app.MapPut("product/{productId}", UpdateProductInfo)
                .RequirePermissions(Permission.Update);
            app.MapPut("product/{productId}/{newQuantity}", UpdateProductQuant)
                .RequirePermissions(Permission.Read);
            app.MapDelete("product/{productId}", UpdateProductQuant)
                .RequirePermissions(Permission.Delete);
            return app;
        }
        public static async Task<IResult> DeleteProduct(int productId, ProductsService service)
        {
            await service.DeleteProduct(productId);
            return Results.Ok("ok");
        }
        public static async Task<IResult> UpdateProductInfo(int productId, UpdateProductRequest request, ProductsService service)
        {
            await service.UpdateProductInfo(productId, request.CategoryName, request.Name, request.Description, request.Price);
            return Results.Ok();
        }

        public static async Task<IResult> UpdateProductQuant(int productId, int newQuantity, ProductsService service)
        {
            await service.UpdateProductQuantity(productId, newQuantity);
            return Results.Ok();
        }

        public static async Task<IResult> AddProduct(AddProductRequest request,
            ProductsService productsService)
        {
            await productsService.AddProduct(request.Name, request.Description, request.Price, request.StockQuantity, request.CategoryName);

            return Results.Ok();
        }
        public static async Task<IResult> GetAllProducts(ProductsService productsService, int pageNumber, int pageSize)
        {
            var products = await productsService.GetAllProducts(pageNumber, pageSize);
            return Results.Ok(products);
        }

        public static async Task<IResult> GetProductById(int productId, ProductsService productsService)
        {
            var product = await productsService.GetProductByIdAsync(productId);

            return Results.Ok(product);
        }

        public static async Task<IResult> GetProductsByCategory(string categoryName, ProductsService productsService, int pageNumber, int pageSize)
        {
            var products = await productsService.GetProductsByCategory(categoryName, pageNumber, pageSize);
            return Results.Ok(products);
        }
    }
}
