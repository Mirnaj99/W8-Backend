

using System.ComponentModel.DataAnnotations;

namespace W8_Backend.Models.UserModels.Input
{
    public class AuthenticationServiceRegisterRequest
    {
       
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        
        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }
        public bool VerifiedUser { get; set; }
      
        public bool DoNotSendVerificationEmail { get; set; }
        [Required]
        public string AppName { get; set; }


    }
}
