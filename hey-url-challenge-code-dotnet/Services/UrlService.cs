using hey_url_challenge_code_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace hey_url_challenge_code_dotnet.Services
{
    public class UrlService : IUrlService
    {
        private IUrlRepository _urlRepository;
        private static readonly Random _random = new Random();

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public IEnumerable<Url> GetAll => _urlRepository.GetAll;

        public IEnumerable<Url> GetUrlAndClicks => _urlRepository.GetUrlAndClicks;

        public void InsertUrl(string shortUrl, string OriginalUrl)
        {
            Url url = new Url();
            url.ShortUrl = shortUrl;
            url.OriginalUrl = OriginalUrl;
            url.CreationDate = DateTime.Now;
            url.Count = 0;
            _urlRepository.Create(url);
        }

        public string ShortenUrl(string url)
        {
            url = Regex.Replace(url, "http(s)?:", "");
            url = Regex.Replace(url, "[/.]+", "");
            return new string(Enumerable.Repeat(url, 5)
                .Select(s => s[_random.Next(s.Length)]).ToArray()).ToUpper();
        }


        public bool CheckUrlRepeated(string shortUrl, IEnumerable<Url> savedUrls)
        {
            return savedUrls.FirstOrDefault(u => u.ShortUrl == shortUrl) != null;
        }

        public Url FindByUrl(string url)
        {
            return _urlRepository.FindByUrl(url);
        }

        public void UpdateUrlCount(string url)
        {
            Url urlObject = _urlRepository.FindByUrl(url);

            urlObject.Count++;

            _urlRepository.Update(urlObject);            
        }


    }
}
