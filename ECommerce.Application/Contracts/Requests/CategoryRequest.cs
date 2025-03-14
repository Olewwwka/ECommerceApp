using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Contracts.Requests
{
    public record CategoryRequest
         (
             [Required] string Name,
             [Required] string Description
         );
}
