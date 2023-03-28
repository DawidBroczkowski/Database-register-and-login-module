using System.ComponentModel.DataAnnotations;

namespace Euvic.Application.Dtos
{
    public record RegisterDto
    {
#pragma warning disable CS8618
        [Required]
        [MinLength(11), MaxLength(11)]
        public string Pesel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [MinLength(3), MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MinLength(6), MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [MinLength(4), MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Range(0, 150)]
        public ushort? Age { get; set; }

        public double? ElectricityConsumption { get; set; }
    }
}
