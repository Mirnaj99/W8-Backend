using System;

namespace W8_Backend.Models.UserModels.Output
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsVerified { get; set; }
        public string JwtToken { get; set; }
        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
 
    }
}
