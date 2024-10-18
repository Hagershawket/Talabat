namespace LinkDev.Talabat.Core.Abstraction.Services.Auth.Models
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
