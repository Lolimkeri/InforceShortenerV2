using InforceShortener.Data.Models;
using InforceShortener.Interfaces;
using InforceShortener.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = InforceShortener.Interfaces.IAuthorizationService;

namespace InforceShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebContentController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IWebContentService _webContentService;

        public WebContentController(IAuthorizationService authorizationService, IWebContentService webContentService)
        {
            _authorizationService = authorizationService;
            _webContentService = webContentService;
        }

        [HttpGet]
        public IActionResult GetWebContent(string textName)
        {
            var textValue = _webContentService.GetWebContentByName(textName).TextValue;
            return Ok(new { textValue });
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public IActionResult EditWebContent([FromBody] EditWebContentDTO editWebContentDTO)
        {
            var username = _authorizationService.GetUsernameByHttpContext(HttpContext);

            var webContent = new WebContent
            {
                TextName = editWebContentDTO.TextName,
                TextValue = editWebContentDTO.TextValue,
                ChangedBy = _authorizationService.GetUsers().FirstOrDefault(u => u.Username == username)
            };

            _webContentService.EditWebContent(webContent);

            return Ok();
        }
    }
}
