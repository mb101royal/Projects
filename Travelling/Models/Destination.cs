using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Travelling.Models
{
    public class Destination
    {
        public int Id { get; set; }
        [Required, Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        [Required, MaxLength(36)]
        public string? Title { get; set; }
        [Required]
        public string? Rating { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
