﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euvic.Domain.Models
{
    public record User
    {
        [Key]
        [Required]
        [MaxLength(11)]
        public string Pesel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Range(0, 150)]
        public ushort Age { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public double? ElectricityConsumption { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(256)]
        public byte[] PasswordSalt { get; set; }
    }
}
