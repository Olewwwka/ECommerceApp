using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Contracts.Requests
{
    public record UpdateProductRequest
    (
         [Required] string Name,
         [Required] string Description,
         [Required] int Price,
         [Required] string CategoryName
     );
}
