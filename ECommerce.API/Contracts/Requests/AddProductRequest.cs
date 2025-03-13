﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Contracts.Requests
{
    public record AddProductRequest
    (
        [Required] string Name,
        [Required] string Description,
        [Required] int Price,
        [Required] int StockQuantity,
        [Required] string CategoryName
    );
}
