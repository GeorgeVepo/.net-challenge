using hey_url_challenge_code_dotnet.Models;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Services
{
    public interface IUrlService
    {
        IEnumerable<Url> GetAll { get; }
        IEnumerable<Url> GetUrlAndClicks { get; }
        void InsertUrl(string shortUrl, string OriginalUrl);
        string ShortenUrl(string url);
        bool CheckUrlRepeated(string shortUrl, IEnumerable<Url> savedUrls);
        void UpdateUrlCount(string url);
        Url FindByUrl(string url);
    }
}
