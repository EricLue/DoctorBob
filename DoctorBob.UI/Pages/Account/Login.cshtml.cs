using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoctorBob.Core.StaffManagement.Domain;

namespace DoctorBob.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        [BindProperty]
        public Credential Credential { get; set; }

        public LoginModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Staff = _context.StaffMembers.ToList();
        }

        public IList<Staff> Staff { get; set; }
        
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            bool userAndPwCorrect = false;
            
            if (Credential.UserName != null && Credential.Password != null)
            {
                foreach (var entity in Staff)
                {
                    if (entity.Username.Equals(Credential.UserName))
                    {
                        if (entity.Password.Equals(Credential.Password))
                        {
                            userAndPwCorrect = true;
                            break;
                        }
                    }
                }
                if (userAndPwCorrect)
                {
                    return new RedirectToPageResult("/Home/HomeView");
                }
                else
                {
                    return new RedirectToPageResult("/Account/Login");
                }
            }
            else
            {
                return new RedirectToPageResult("/Account/Login");
            }
        }
    }

    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
