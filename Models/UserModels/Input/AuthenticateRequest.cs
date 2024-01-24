

namespace W8_Backend.Models.UserModels.Input
{
    public class AuthenticateRequest
    {   
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AppName { get; set; }

    }
}
