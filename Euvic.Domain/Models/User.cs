using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euvic.Domain.Models
{
    public record User
    {
#pragma warning disable CS8618
        [Key]
        [Required]
        [MaxLength(11)]
        [Column(TypeName = "varchar(11)")]
        public string Pesel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(320)]
        [Column(TypeName = "varchar(320)")]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; }

        [Range(0, 150)]
        public ushort Age { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public double? ElectricityConsumption { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; }
    }
}
