using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private IUrlService _UrlService;
        private IClickService _clickService;


        public UrlsController(ILogger<UrlsController> logger, IUrlService urlService, IClickService clickService)
        {
            _UrlService = urlService;
            _logger = logger;
            _clickService = clickService;
        }   
     

        public IActionResult Index()
        {
            return View(GetIndextHomeViewModel());
        }


        [Route("urls/create")]
        public IActionResult Create(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("index", GetIndextHomeViewModel());
            }

            IEnumerable<Url> savedUrls = _UrlService.GetAll;

            string shortUrl = _UrlService.ShortenUrl(model.NewUrl.OriginalUrl);
            while (_UrlService.CheckUrlRepeated(shortUrl, savedUrls))
            {
                shortUrl = _UrlService.ShortenUrl(model.NewUrl.OriginalUrl);
            }

            _UrlService.InsertUrl(shortUrl, model.NewUrl.OriginalUrl);

            return View("index", GetIndextHomeViewModel());
        }

        [Route("/NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }


        [Route("/{url}")]
        public IActionResult Visit(string url)
        {
            _UrlService.UpdateUrlCount(url);

            return View("index", GetIndextHomeViewModel());
        }

        [Route("urls/{url}")]
        public IActionResult Show(string url)
        {
            Url urlObject = _UrlService.FindByUrl(url);
            _clickService.InsertClick(urlObject);

            IEnumerable<Click> clickList = _clickService.GetCurrentMonthClicks();

            Dictionary<string, int> dailyClicks = _clickService.GetDailyClicks(clickList);

            Dictionary<string, int> browseClicks = _clickService.GetBrowserClicks(clickList);

            Dictionary<string, int> platformClicks = _clickService.GetPlatformClicks(clickList);            

            return View(GetShowViewModel(urlObject, dailyClicks, platformClicks, browseClicks));
        }

        private ShowViewModel GetShowViewModel(Url urlObject, Dictionary<string, int> dailyClicks, Dictionary<string, int> platformClicks, Dictionary<string, int> browseClicks)
        {
            ShowViewModel showViewModel = new ShowViewModel();
            showViewModel.Url = urlObject;
            showViewModel.DailyClicks = dailyClicks;
            showViewModel.PlatformClicks = platformClicks;
            showViewModel.BrowseClicks = browseClicks;
            return showViewModel;
        }


        private HomeViewModel GetIndextHomeViewModel()
        {
            var model = new HomeViewModel();

            model.Urls = _UrlService.GetAll;
            model.NewUrl = new();
            return model;
        }

    }
}