using ECommerce.API.Extensions;
using ECommerce.Application.Contracts.Requests;
using ECommerce.Application.Services;
using ECommerce.Core.Enums;

namespace ECommerce.API.Endpoints
{
    public static class CartEndpoints
    {
        public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("cartItem/{itemId}/{quantity}", AddItemToCart);
            app.MapPost("updateQuantity/{itemId}/{quantity}", UpdateItemQuantity);
            app.MapDelete("cartItem/{itemId}", RemoveItemFromCart);
            app.MapGet("cartItems", GetCartItems);
            return app;
        }
        public static async Task<IResult> RemoveItemFromCart(int itemId, UserCartService userCartService)
        {
            await userCartService.RemoveProductFromCart(itemId);
            return Results.Ok("ok");
        }
        public static async Task<IResult> UpdateItemQuantity(int itemId, int quantity, UserCartService userCartService)
        {
            await userCartService.UpdateQuantityOfProduct(itemId, quantity);
            return Results.Ok("ok");
        }

        public static async Task<IResult> AddItemToCart(int itemId, int quantity, UserCartService userCartService)
        {
            await userCartService.AddProductToCart(itemId, quantity);
            return Results.Ok("ok");
        }
        public static async Task<IResult> GetCartItems(UserCartService userCartService)
        {
            var products = await userCartService.GetCartProducts();
            return Results.Ok(products);
        }
    }
}
