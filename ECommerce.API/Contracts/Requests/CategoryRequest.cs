using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Contracts.Requests
{
    public record CategoryRequest
         (
             [Required] string Name,
             [Required] string Description
         );
}
