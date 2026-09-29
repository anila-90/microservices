
namespace platformservice.viewmodels
{
    public class AuthResponse
    {
      
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
        public required string Type {get; set;}
    }
    public class AuthRequest
    {
      
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
