using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Contracts.Requests
{
    public record LoginUserRequest
        (
            [Required] string Email,
            [Required] string Password
        );
}
