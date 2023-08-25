namespace InforceShortener.Data.Models
{
    public class User : IModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }


        public virtual List<UrlRecord> UrlRecords { get; set; }

        public virtual List<WebContent> WebContents { get; set; }
    }
}
