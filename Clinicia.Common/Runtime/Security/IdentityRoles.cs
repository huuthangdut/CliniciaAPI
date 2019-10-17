namespace Clinicia.Common.Runtime.Security
{
    public class IdentityRoles
    {
        public static readonly string SuperAdmin = "SuperAdmin";
        public static readonly string PracticeAdmin = "PracticeAdmin";
        public static readonly string Doctor = "Doctor";
        public static readonly string Patient = "Patient";

        public static readonly string[] AllRoles =
        {
            SuperAdmin,
            PracticeAdmin,
            Doctor,
            Patient
        };
    };
}
