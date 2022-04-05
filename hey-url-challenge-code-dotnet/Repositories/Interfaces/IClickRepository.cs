using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Models
{
    public interface IClickRepository
    {
        IEnumerable<Click> GetAll { get; }
        void Create(Click click);
    }
}
