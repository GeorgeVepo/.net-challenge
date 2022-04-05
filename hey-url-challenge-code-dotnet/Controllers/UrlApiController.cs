using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UrlApiController : ControllerBase
    {
        private IUrlService _UrlService;

        public UrlApiController(IUrlService urlService)
        {
            _UrlService = urlService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Url>> GetUrls()
        {
            List<Url> urls = new List<Url>();
            urls.AddRange(_UrlService.GetUrlAndClicks);

            if(urls.Count > 10)
                urls.RemoveRange(0, (urls.Count - 10));

            return CreatedAtAction(nameof(GetUrls), urls);
        }
    }
}
