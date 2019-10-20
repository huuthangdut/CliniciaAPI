namespace Clinicia.Common.Runtime.Security
{
    public class IdentityRoles
    {
        public static readonly string Admin = "Admin";
        public static readonly string Doctor = "Doctor";
        public static readonly string Patient = "Patient";

        public static readonly string[] AllRoles =
        {
            Admin,
            Doctor,
            Patient
        };
    };
}
