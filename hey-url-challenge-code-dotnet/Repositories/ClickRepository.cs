using HeyUrlChallengeCodeDotnet.Data;
using System.Collections.Generic;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Models
{
    public class ClickRepository : IClickRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ClickRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IEnumerable<Click> GetAll => _applicationContext.Clicks;

        public void Create(Click click)
        {
            _applicationContext.Add(click);
            _applicationContext.SaveChanges();
        }
    }
}
