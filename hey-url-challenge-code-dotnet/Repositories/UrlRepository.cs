using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Models
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApplicationContext _applicationContext;

        public UrlRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IEnumerable<Url> GetAll => _applicationContext.Urls;

        public IEnumerable<Url> GetUrlAndClicks => _applicationContext.Urls.Include(u => u.Clicks);

        public Url FindByUrl(string url)
        {
            return _applicationContext.Urls.FirstOrDefault(u => u.ShortUrl == url);
        }

        public void Create(Url url)
        {
            _applicationContext.Add(url);
            _applicationContext.SaveChanges();
        }

        public void Update(Url url)
        {
            _applicationContext.Update(url);
            _applicationContext.SaveChanges();
        }
    }
}
