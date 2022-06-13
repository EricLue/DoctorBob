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
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Patient = _context.Patients.ToList();
        }

        public IList<Patient> Patient { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            var entity = from e in _context.Patients
                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString.Contains("Aktiv") || searchString.Contains("aktiv"))
                {
                    entity = entity.Where(e => e.Active);
                }
                else
                {
                    entity = entity.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));
                }
            }

            Patient = await entity
                .Include(p => p.Gender)
                .Include(p => p.Room)
                .Include(p => p.Therapy)
                .Include(p => p.CaringStaff).ToListAsync();
        }

        public String GetPatientName(int Id)
        {
            string Name = _context.Patients.Find(Id).FirstName;
            Name += " ";
            Name += _context.Patients.Find(Id).LastName;
            return Name;
        }

        public String GetTherapyName(int Id)
        {
            return _context.Therapies.Find(Id).Name;
        }

        public String GetStaffName(int Id)
        {
            string Name = _context.StaffMembers.Find(Id).FirstName.Substring(0,2);
            Name += ". ";
            Name += _context.StaffMembers.Find(Id).LastName;
            return Name;
        }
    }
}
