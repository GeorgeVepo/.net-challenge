using hey_url_challenge_code_dotnet.Models;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Services
{
    public interface IClickService
    {
        void InsertClick(Url urlObject);
        IEnumerable<Click> GetCurrentMonthClicks();
        Dictionary<string, int> GetDailyClicks(IEnumerable<Click> clickList);
        Dictionary<string, int> GetBrowserClicks(IEnumerable<Click> clickList);

        Dictionary<string, int> GetPlatformClicks(IEnumerable<Click> clickList);
    }
}
