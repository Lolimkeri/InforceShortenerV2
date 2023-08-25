using InforceShortener.Data.Models;

namespace InforceShortener.Interfaces
{
    public interface IUrlService
    {
        IQueryable<UrlRecord> GetUrlRecords();

        UrlRecord GetUrlRecord(int id);

        void AddUrlRecord(UrlRecord urlRecord);

        void DeleteUrlRecord(int id);

        string CreateShortUrl(string originalUrl);

        UrlRecord FindByOriginalUrl(string originalUrl);

        UrlRecord FindByShortUrl(string shortUrl);
    }
}
