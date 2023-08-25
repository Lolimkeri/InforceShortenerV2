namespace InforceShortener.Data.Models
{
    public class UrlRecord : IModel
    {
        public int Id { get; set; }

        public string OriginalUrl { get; set; }

        public string ShortUrl { get; set; }

        public DateTime CreatedDate { get; set; }


        public virtual User User { get; set; }
    }
}
