namespace Clinicia.Common.Enums
{
    public enum LoginResultType: byte
    {
        Success = 1,
        InvalidUserNameOrPassword,
        UserIsNotActive,
        UserLockedOut,
        RequireConfirmedPhoneNumber
    }
}