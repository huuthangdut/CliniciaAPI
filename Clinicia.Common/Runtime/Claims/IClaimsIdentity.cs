namespace Clinicia.Common.Runtime.Claims
{
    public interface IClaimsIdentity
    {
        int UserId { get; }

        int? GetUserId();

        string UserName { get; }

        string[] CurrentUserRoles { get; }

        bool CurrentUserIsInRole(string roleName);
    }
}