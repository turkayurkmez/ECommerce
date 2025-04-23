using MediatR;

namespace ECommerce.Identity.Domain.Events
{
    public record UserCreatedDomainEvent(Guid UserId, string UserName, string Email) : INotification;
    public record UserUpdatedDomainEvent(Guid UserId, string UserName, string Email) : INotification;
    public record UserDeletedDomainEvent(Guid UserId, string UserName) : INotification;
    //user password changed
    public record UserPasswordChangedDomainEvent(Guid UserId, string UserName) : INotification;
    //user email confirmed
    public record UserEmailConfirmedDomainEvent(Guid UserId, string UserName, string Email) : INotification;
    //user role added
    public record UserRoleAddedDomainEvent(Guid UserId, string RoleName) : INotification;
    //user role removed
    public record UserRoleRemovedDomainEvent(Guid UserId, string RoleName) : INotification;

    public record RoleCreatedDomainEvent(Guid RoleId, string RoleName) : INotification;

    public record RoleUpdatedDomainEvent(Guid RoleId, string RoleName) : INotification;

    public record RoleDeletedDomainEvent(Guid RoleId, string RoleName) : INotification;
}
