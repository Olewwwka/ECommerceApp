using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Contracts.Requests
{
    public record LoginUserRequest
        (
            [Required] string Email,
            [Required] string Password
        );
}
