using W8_Backend.Data.Entities;
using System.Collections.Generic;

namespace W8_Backend.Models.UserModels.Output
{
    public class UserView
    {
        public Users User { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public bool IsAdmin { get; set; }
  

    }
}
