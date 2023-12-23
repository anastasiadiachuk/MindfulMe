using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Offer
    {
        public int OfferId { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}
