using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace W8_Backend.Data.Entities
{
    public class Users
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string MicroUserID { get; set; }

        public string? FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]

        public string? LastName { get; set; }
        [Column(TypeName = "varchar(100)")]

        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }



    }
}






