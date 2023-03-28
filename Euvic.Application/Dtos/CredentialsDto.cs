namespace Euvic.Application.Dtos
{
    public record CredentialsDto
    {
        #pragma warning disable CS8618
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
