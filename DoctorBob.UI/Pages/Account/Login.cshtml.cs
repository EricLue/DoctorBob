using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorBob.UI.Pages.Account
{
    public class LoginModel : PageModel
    {

        public Credential Credential { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Method is called when clicked "Login"
        }
    }

    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
