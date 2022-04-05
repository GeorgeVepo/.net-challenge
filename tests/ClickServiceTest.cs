using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services;
using Moq;
using NUnit.Framework;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    internal class ClickServiceTest
    {
        ClickService _clickService;
        List<Click> _clickList = new List<Click>()
        {
            new Click() {
                Id = Guid.NewGuid(),
                Browsers = "Chrome",
                OS = "Windows",
                ClickDate = DateTime.Now,
                Url = new Url()
            },
            new Click() {
                Id = Guid.NewGuid(),
                Browsers = "IE",
                OS = "Windows",
                ClickDate = DateTime.Now,
                Url = new Url()
            },
            new Click() {
                Id = Guid.NewGuid(),
                Browsers = "Firefox",
                OS = "Ubuntu",
                ClickDate = DateTime.Now,
                Url = new Url()
            }
        };

        Dictionary<string, int> _browseClicks = new Dictionary<string, int>()
        {
            { "Chrome", 1 },
            { "Safari", 0 },
            { "Firefox", 1 },
            { "IE", 1 }
        };

        Dictionary<string, int> _platformClicks = new Dictionary<string, int>()
        {
            { "Windows", 2 },
            { "macOS", 0 },
            { "Other", 0 },
            { "Ubuntu", 1 }
        };

        Dictionary<string, int> _dailyClicks = new Dictionary<string, int>()
        {
            { "0", 3 }
        };

        Url _url = new Url()
        {
            Id = new Guid(),
            Clicks = new List<Click>(),
            Count = 1,
            CreationDate = DateTime.Now,
            OriginalUrl = "http://teste1.teste/valido",
            ShortUrl = "GWSKA"

        };

        Click _click = new Click()
        {
            ClickDate = DateTime.Now,
            Browsers = "Chrome",
            OS = "Windows",
            Url = new Url()
            {
                Id = new Guid(),
                Clicks = new List<Click>(),
                Count = 1,
                CreationDate = DateTime.Now,
                OriginalUrl = "http://teste1.teste/valido",
                ShortUrl = "GWSKA"

            }
    };


        [SetUp]
        public void Setup()
        {
            Mock<IBrowserDetector> mockBrowserClick = new Mock<IBrowserDetector>();
            Mock<IClickRepository> mockClickRepository = new Mock<IClickRepository>();
            mockBrowserClick.Setup(m => m.Browser.Name).Returns("Chrome");
            mockBrowserClick.Setup(m => m.Browser.OS).Returns("Windows");

            mockClickRepository.Setup(m => m.GetAll).Returns(_clickList);

            mockClickRepository.Setup(m => m.Create(_click));
            _clickService = new ClickService(mockClickRepository.Object, mockBrowserClick.Object);
        }

        [Test]
        public void TestGetCurrentMonthClicks()
        {
            var clickList = _clickService.GetCurrentMonthClicks();
            Assert.AreEqual(clickList, _clickList);
        }

        [Test]
        public void TestGetBrowserClicks()
        {
            var browserClicks = _clickService.GetBrowserClicks(_clickList);
            Assert.AreEqual(browserClicks, _browseClicks);
        }

        [Test]
        public void TestGetPlatformClicks()
        {
            var platformClicks = _clickService.GetPlatformClicks(_clickList);
            Assert.AreEqual(platformClicks, _platformClicks);
        }

        [Test]
        public void TestGetDailyClicks()
        {
            var dailyClicks = _clickService.GetDailyClicks(_clickList);
            Assert.AreEqual(dailyClicks, _dailyClicks);
        }

        [Test]
        public void TestInsertClick()
        {
            _clickService.InsertClick(_url);
            Assert.Pass();
        }
    }
}
