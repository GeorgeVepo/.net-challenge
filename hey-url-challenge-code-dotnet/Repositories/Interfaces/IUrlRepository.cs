using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Models
{
    public interface IUrlRepository
    {
        IEnumerable<Url> GetAll { get; }

        IEnumerable<Url> GetUrlAndClicks{ get; }

        Url FindByUrl(string url);     
        void Create(Url url);

        void Update(Url url);
    }
}
