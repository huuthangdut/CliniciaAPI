namespace Clinicia.Common.Helpers
{
    public class AppSettings
    {
        public string DefaultLanguage { get; set; }

        public int LockoutTimeMinutes { get; set; }

        public int AccessTokenLifeTimeHours { get; set; }

        public string GoogleApiKey { get; set; }

        public string FCMServerKey { get; set; }

        public string FCMSenderID { get; set; }
    }
}