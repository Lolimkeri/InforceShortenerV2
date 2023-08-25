using InforceShortener.Data.Models;

namespace InforceShortener.Interfaces
{
    public interface IWebContentService
    {
        WebContent GetWebContentByName(string textName);

        void EditWebContent(WebContent webContent);
    }
}
