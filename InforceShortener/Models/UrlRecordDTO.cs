using InforceShortener.Data.Models;

namespace InforceShortener.Models
{
    public class UrlRecordDTO
    {
        public int Id { get; set; }

        public string OriginalUrl { get; set; }

        public string ShortUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserName { get; set; }

        public string UserRole { get; set; }
    }
}
