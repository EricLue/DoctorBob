using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.StaffManagement.Domain;

namespace DoctorBob.UI.Pages.StaffManager
{
    public class DetailsModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DetailsModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.StaffMembers.FirstOrDefaultAsync(m => m.Id == id);

            if (Staff == null)
            {
                return NotFound();
            }
            return Page();
        }

        public String GetRoleName(int id)
        {
            return _context.Roles.Find(id).Name;
        } 
    }
}
