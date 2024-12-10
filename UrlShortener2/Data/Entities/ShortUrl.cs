using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener2.Data.Entities
{
    public class ShortUrl : BaseEntity
    {
        [Required]
        public string OriginalUrlString { get; set; } = string.Empty;

        [Required]
        public string ShortUrlString { get; set; } = string.Empty;

        public int Clicks { get; set; } = 0;


        [ForeignKey(nameof(IdentityUser))]
        public string UserId { get; set; } = string.Empty;

        public IdentityUser User { get; set; } = null!;
    }
}
