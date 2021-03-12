using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio_Petar.Models
{
    public class RegisterViewModel{
        public string Username { get; set; }//makes tem from fields to properties
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

    }
}