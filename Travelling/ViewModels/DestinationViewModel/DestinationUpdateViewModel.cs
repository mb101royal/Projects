using System.ComponentModel.DataAnnotations;

namespace Travelling.ViewModels.DestinationViewModel
{
    public class DestinationUpdateViewModel
    {
        [Required]
        public string? ImageUrl { get; set; }
        [Required, MaxLength(36)]
        public string? Title { get; set; }
        [Required]
        public string? Rating { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
