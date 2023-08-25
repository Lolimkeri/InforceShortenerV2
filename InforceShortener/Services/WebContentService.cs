using InforceShortener.Data.Models;
using InforceShortener.Data;
using InforceShortener.Interfaces;

namespace InforceShortener.Services
{
    public class WebContentService: IWebContentService
    {
        private readonly Repository<WebContent> _webContentRepository;

        public WebContentService(Repository<WebContent> webContentRepository)
        {
            _webContentRepository = webContentRepository;
        }

        public WebContent GetWebContentByName(string textName)
        {
            return _webContentRepository.GetAll().FirstOrDefault(w => w.TextName == textName);
        }

        public void EditWebContent(WebContent webContent)
        {
            if (webContent.Id == default)
            {
                var oldWebContent = _webContentRepository.GetAll().Single(w => w.TextName == webContent.TextName);
                webContent.Id= oldWebContent.Id;
            }

            _webContentRepository.Update(webContent);
            _webContentRepository.Save();
        }
    }
}
