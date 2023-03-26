using System.ComponentModel.DataAnnotations;

namespace Euvic.Application.Dtos
{
    public record LoginDto
    {
        [Required]
        [MinLength(3), MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MinLength(6), MaxLength(30)]
        public string Password { get; set; }
    }
}
