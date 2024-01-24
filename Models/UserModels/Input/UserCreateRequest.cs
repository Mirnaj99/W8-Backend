

using System;
using System.ComponentModel.DataAnnotations;

namespace W8_Backend.Models.UserModels.Input
{
    public class UserCreateRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }      
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
   
    }
}
