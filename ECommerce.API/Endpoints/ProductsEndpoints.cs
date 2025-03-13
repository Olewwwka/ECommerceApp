using ECommerce.API.Contracts.Requests;
using ECommerce.Application.Services;
using ECommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace ECommerce.API.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("add product", AddProduct);
            app.MapGet("get products", GetAllProducts);
            app.MapGet("getById/{productId}", GetProductById);
            app.MapGet("by category/{categoryName}", GetProductsByCategory);
            app.MapPut("update product /{productId}", UpdateProductInfo);
            app.MapPut("update product qunt/{productId}/{newQuantity}", UpdateProductQuant);
            return app;
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
        public static async Task<IResult> GetAllProducts(ProductsService productsService)
        {
            var products = await productsService.GetAllProducts();

            return Results.Ok(products);
        }

        public static async Task<IResult> GetProductById(int productId, ProductsService productsService)
        {
            var product = await productsService.GetProductByIdAsync(productId);

            return Results.Ok(product);
        }

        public static async Task<IResult> GetProductsByCategory(string categoryName, ProductsService productsService)
        {

            var products = await productsService.GetProductsByCategory(categoryName);
            return Results.Ok(products);
        }
    }
}
