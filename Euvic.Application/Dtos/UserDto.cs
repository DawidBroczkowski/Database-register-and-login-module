namespace Euvic.Application.Dtos
{
    public record UserDto
    {
#pragma warning disable CS8618
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ushort Age { get; set; }
        public double? ElectricityConsumption { get; set; }
    }
}
