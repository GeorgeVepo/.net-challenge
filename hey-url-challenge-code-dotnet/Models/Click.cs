using System;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Click
    {
        public Guid Id { get; set; }
        public string Browsers { get; set; }
        public string OS { get; set; }
        public DateTime ClickDate { get; set; }
        public Url Url { get; set; }
    }
}
