namespace Clinicia.Entities.Common
{
    public class FileDescription
    {
        public string Guid { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }
    }
}