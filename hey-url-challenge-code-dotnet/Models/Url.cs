using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "URL is required.")]
        [RegularExpression("http(s)?://[a-zA-Z0-9/.]+", ErrorMessage = "Url inválida.")]
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public int Count { get; set; }
        public ICollection<Click> Clicks { get; set; }
    }
}
