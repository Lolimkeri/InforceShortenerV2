using InforceShortener.Configuration;
using InforceShortener.Data;
using InforceShortener.Data.Models;
using InforceShortener.Interfaces;

namespace InforceShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly Repository<UrlRecord> _urlRepository;

        public UrlService(Repository<UrlRecord> urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public IQueryable<UrlRecord> GetUrlRecords()
        {
            return _urlRepository.GetAll();
        }

        public UrlRecord GetUrlRecord(int id)
        {
            return _urlRepository.GetById(id);
        }

        public void AddUrlRecord(UrlRecord urlRecord)
        {
            _urlRepository.Insert(urlRecord);
            _urlRepository.Save();
        }

        public void DeleteUrlRecord(int id)
        {
            _urlRepository.Delete(id);
            _urlRepository.Save();
        }

        public string CreateShortUrl(string originalUrl)
        {
            if (FindByOriginalUrl(originalUrl) != null)
            {
                throw new ArgumentException("Model with this url already exists");
            }

            var shortUrl = Guid.NewGuid().ToString("N").Substring(0, Constants.SHORT_URL_LENGHT);

            int retryNumber = 0;
            while(FindByShortUrl(shortUrl) != null)
            {
                shortUrl = CreateShortUrl(originalUrl);

                if(retryNumber >= Constants.CREATE_SHORT_URL_RETRY_COUNT)
                {
                    throw new Exception("Something went wrong. Please, try again later");
                }

                retryNumber++;
            }

            return shortUrl;
        }

        public UrlRecord FindByOriginalUrl(string originalUrl)
        {
            return GetUrlRecords().FirstOrDefault(u => u.OriginalUrl == originalUrl);
        }

        public UrlRecord FindByShortUrl(string shortUrl)
        {
            return GetUrlRecords().FirstOrDefault(u => u.ShortUrl == shortUrl);
        }
    }
}
