using Newtonsoft.Json;

namespace Clinicia.Dtos.Common
{
    public class FcmPayloadNotification
    {
        [JsonProperty("notification")]
        public FcmNotification Notification { get; set; }

        [JsonProperty("data")]
        public FcmDataNotification Data { get; set; }
    }

    public class FcmNotification
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class FcmDataNotification
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("notificationDate")]
        public string NotificationDate { get; set; }
    }
}
