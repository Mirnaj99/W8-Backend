

using System.ComponentModel.DataAnnotations;

namespace W8_Backend.Models.UserModels.Input
{
    public class RefreshTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
