using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.UI.Pages.PatientManager
{
    public class DetailsModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DetailsModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient = await _context.Patients
                .Include(p => p.Room)
                .Include(p => p.Therapy)
                .Include(p => p.CaringStaff).FirstOrDefaultAsync(m => m.Id == id);

            if (Patient == null)
            {
                return NotFound();
            }
            return Page();
        }

        public String GetTherapyName(int Id)
        {
            return _context.Therapies.Find(Id).Name;
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
