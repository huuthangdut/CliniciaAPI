using System;

namespace Clinicia.Common.Runtime.Claims
{
    public interface IClaimsIdentity
    {
        Guid UserId { get; }

        Guid? GetUserId();

        string UserName { get; }

        string[] CurrentUserRoles { get; }

        bool CurrentUserIsInRole(string roleName);
    }
}