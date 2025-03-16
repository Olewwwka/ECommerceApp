namespace ECommerce.Core.Abstractions.ServicesInterfaces
{
    public interface ICurrentUserService
    {
        string? Email { get; }
        List<string> Roles { get; }
        int? UserId { get; }
    }
}