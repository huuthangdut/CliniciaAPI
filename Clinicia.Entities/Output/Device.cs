using System;

namespace Clinicia.Dtos.Output
{
    public class Device
    {
        public Guid Id { get; set; }

        public string DeviceType { get; set; }

        public string DeviceToken { get; set; }
    }
}
