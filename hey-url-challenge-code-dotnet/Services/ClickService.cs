using hey_url_challenge_code_dotnet.Models;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Services
{
    public class ClickService : IClickService
    {
        IClickRepository _clickRepository;
        private readonly IBrowserDetector _browserDetector;
        public ClickService(IClickRepository clickRepository, IBrowserDetector browserDetector)
        {
            _clickRepository = clickRepository;
            _browserDetector = browserDetector;
        }

        public void InsertClick(Url urlObject)
        {
            Click click = new Click();
            click.ClickDate = DateTime.Now;
            click.Browsers = _browserDetector.Browser.Name;
            click.OS = _browserDetector.Browser.OS;
            click.Url = urlObject;
            _clickRepository.Create(click);
        }

        public IEnumerable<Click> GetCurrentMonthClicks()
        {
            IEnumerable<Click> clickList = _clickRepository.GetAll;
            return clickList.Where(click => click.ClickDate.Month == DateTime.Now.Month);
        }

        public Dictionary<string, int> GetDailyClicks(IEnumerable<Click> clickList)
        {
            clickList = clickList.Where(click => click.ClickDate.Month == DateTime.Now.Month);

            Dictionary<string, int> dailyClicks = new Dictionary<string, int>();

            int i = 0;

            clickList.GroupBy(click => click.ClickDate.Day).ToList().ForEach(group =>
            {
                dailyClicks.Add((i++).ToString(), group.Count());
            });

            return dailyClicks;
        }

        public Dictionary<string, int> GetBrowserClicks(IEnumerable<Click> clickList)
        {
            Dictionary<string, int> browseClicks = new Dictionary<string, int>();
            browseClicks.Add("IE", clickList.Where(click => click.Browsers == "IE").Count());
            browseClicks.Add("Firefox", clickList.Where(click => click.Browsers == "Firefox").Count());
            browseClicks.Add("Chrome", clickList.Where(click => click.Browsers == "Chrome").Count());
            browseClicks.Add("Safari", clickList.Where(click => click.Browsers == "Safari").Count());

            return browseClicks;
        }

        public Dictionary<string, int> GetPlatformClicks(IEnumerable<Click> clickList)
        {
            Dictionary<string, int> platformClicks = new Dictionary<string, int>();
            platformClicks.Add("Windows", clickList.Where(click => click.OS == "Windows").Count());
            platformClicks.Add("macOS", clickList.Where(click => click.OS == "macOS").Count());
            platformClicks.Add("Ubuntu", clickList.Where(click => click.OS == "Ubuntu").Count());
            platformClicks.Add("Other", clickList.Where(click => click.OS == "Other").Count());

            return platformClicks;
        }
    }

}
