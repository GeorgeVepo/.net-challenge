using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace tests
{
    public class UrlServiceTest
    {
        UrlService _urlService;
        string _validUrl = "http://tesste.valid/teste";
        string _shortUrl = "RWDAS";
        Url _url = new Url()
        {
            Id = new Guid(),
            Clicks = new List<Click>(),
            Count = 1,
            CreationDate = DateTime.Now,
            OriginalUrl = "http://teste1.teste/valido",
            ShortUrl = "GWSKA"

        };

        List<Url> _urlList = new List<Url>()
            {
                new Url(){
                   Id = new Guid(),
                   Clicks = new List<Click>(),
                   Count = 1,
                   CreationDate = DateTime.Now,
                   OriginalUrl = "http://teste1.teste/valido",
                   ShortUrl = "GWSKA"

                }, new Url(){
                   Id = new Guid(),
                   Clicks = new List<Click>(),
                   Count = 2,
                   CreationDate = DateTime.Now,
                   OriginalUrl = "http://teste2.teste/valido",
                   ShortUrl = "ENFLW"

                }, new Url(){
                   Id = new Guid(),
                   Clicks = new List<Click>(),
                   Count = 3,
                   CreationDate = DateTime.Now,
                   OriginalUrl = "http://teste3.teste/valido",
                   ShortUrl = "MFJSO"

                },
            };

        

        [SetUp]
        public void Setup()
        {
            Mock<IUrlRepository> mock = new Mock<IUrlRepository>();
            mock.Setup(m => m.Create(new Url()));
            mock.Setup(m => m.GetAll).Returns(_urlList);
            mock.Setup(m => m.GetUrlAndClicks).Returns(_urlList);
            mock.Setup(m => m.FindByUrl(_shortUrl)).Returns(_url);
            _urlService = new UrlService(mock.Object);

        }

        [Test]
        public void TestGetAll()
        {
            var result = _urlService.GetAll;
            Assert.AreEqual(result, _urlList);
        }

        [Test]
        public void TestGetUrlAndClicks()
        {
            var result = _urlService.GetUrlAndClicks;
            Assert.AreEqual(result, _urlList);
        }

        [Test]
        public void TestInsertUrl()
        {            
            _urlService.InsertUrl(_shortUrl, _validUrl);
            Assert.Pass();
        }

        [Test]
        public void TestGetShortenUrl()
        {
            string shortUrl =_urlService.ShortenUrl(_validUrl);
            Assert.AreEqual(shortUrl.Length, 5);
        }

        [Test]
        public void TestUpdateUrlCount()
        {
            _urlService.UpdateUrlCount(_shortUrl);
            Assert.Pass();
        }

        [Test]
        public void TestFindByUrlTrue()
        {
            Url url = _urlService.FindByUrl(_shortUrl);
            Assert.AreEqual(url, _url);
        }

        [Test]
        public void TestFindByUrlFalse()
        {
            Url url = _urlService.FindByUrl("Teste");
            Assert.IsNull(url);
        }

        [Test]
        public void TestCheckUrlRepeatedTrue()
        {
            Assert.That(_urlService.CheckUrlRepeated(_url.ShortUrl, _urlList));
        }

        [Test]
        public void TestCheckUrlRepeatedFalse()
        {
            Assert.IsFalse(_urlService.CheckUrlRepeated(_shortUrl, _urlList));
        }

    }
}