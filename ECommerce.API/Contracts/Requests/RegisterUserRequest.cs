﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Contracts.Requests
{
    public record RegisterUserRequest
    (
        [Required] string Login,
        [Required] string Password,
        [Required] string Email,
        [Required] string FirstName,
        [Required] string LastName
    );
}
