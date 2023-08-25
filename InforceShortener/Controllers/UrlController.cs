using InforceShortener.Data.Models;
using InforceShortener.Interfaces;
using InforceShortener.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IAuthorizationService = InforceShortener.Interfaces.IAuthorizationService;

namespace InforceShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;
        private readonly IAuthorizationService _authorizationService;

        public UrlController(IUrlService urlService, IAuthorizationService authorizationService)
        {
            _urlService = urlService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public IActionResult GetUrls()
        {
            var resultList = new List<UrlRecordDTO>();

            foreach(var urlRecord in _urlService.GetUrlRecords().ToList())
            {
                resultList.Add(new UrlRecordDTO
                {
                    Id = urlRecord.Id,
                    OriginalUrl = urlRecord.OriginalUrl,
                    ShortUrl = urlRecord.ShortUrl,
                    CreatedDate = urlRecord.CreatedDate,
                    UserName = urlRecord.User.Username,
                    UserRole = urlRecord.User.Role
                });
            }

            return Ok(resultList);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateShortUrl([FromBody] CreateShortUriDTO createShortUriDTO)
        {
            var shortUrl = _urlService.CreateShortUrl(createShortUriDTO.OriginalUrl);

            var username = _authorizationService.GetUsernameByHttpContext(HttpContext);

            var urlRecord = new UrlRecord
            {
                OriginalUrl = createShortUriDTO.OriginalUrl,
                ShortUrl = shortUrl,
                CreatedDate = DateTime.Now,
                User = _authorizationService.GetUsers().FirstOrDefault(u => u.Username == username)
            };

            _urlService.AddUrlRecord(urlRecord);

            var newUrlRecord = _urlService.FindByOriginalUrl(createShortUriDTO.OriginalUrl);

            if (newUrlRecord == null)
            {
                return BadRequest("Something went wrong. Please, try again later");
            }

            var urlRecordDTO = new UrlRecordDTO
            {
                Id = newUrlRecord.Id,
                OriginalUrl = newUrlRecord.OriginalUrl,
                ShortUrl = newUrlRecord.ShortUrl,
                CreatedDate = newUrlRecord.CreatedDate,
                UserName = newUrlRecord.User.Username,
                UserRole = newUrlRecord.User.Role
            };

            return Ok( urlRecordDTO );
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUrlInfo(int id)
        {
            var urlRecord = _urlService.GetUrlRecord(id);

            var urlRecordDTO = new UrlRecordDTO
            {
                Id = urlRecord.Id,
                OriginalUrl = urlRecord.OriginalUrl,
                ShortUrl = urlRecord.ShortUrl,
                CreatedDate = urlRecord.CreatedDate,
                UserName = urlRecord.User.Username,
                UserRole = urlRecord.User.Role
            };

            return Ok(urlRecordDTO);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUrl(int id)
        {
            var urlRecord = _urlService.GetUrlRecord(id);

            var username = _authorizationService.GetUsernameByHttpContext(HttpContext);
            var role = _authorizationService.GetRoleByHttpContext(HttpContext);

            if (username != urlRecord.User.Username && role != "Admin")
            {
                return BadRequest("You don`t have permission to delete this record");
            }

            _urlService.DeleteUrlRecord(id);

            return Ok();
        }
    }
}
