using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.TherapyManagement.Domain;

namespace DoctorBob.UI.Pages.TherapyManager
{
    public class DetailsModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DetailsModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public Therapy Therapy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Therapy = await _context.Therapies
                .Include(t => t.Drug)
                .Include(t => t.TimeModel).FirstOrDefaultAsync(m => m.Id == id);

            if (Therapy == null)
            {
                return NotFound();
            }
            return Page();
        }

        public String GetDrugName(int Id)
        {
            return _context.Drugs.Find(Id).Name;
        }

        public String GetIntakeCategoryName(int Id)
        {
            return _context.IntakeCategories.Find(Id).Name;
        }

        public String GetStaffName(int Id)
        {
            string Name = _context.StaffMembers.Find(Id).FirstName.Substring(0, 2);
            Name += ". ";
            Name += _context.StaffMembers.Find(Id).LastName;
            return Name;
        }
    }
}
