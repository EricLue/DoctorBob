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
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Staff = _context.StaffMembers.ToList();
        }

        public IList<Staff> Staff { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            var entity = from e in _context.StaffMembers
                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                entity = entity.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString)
                || e.Username.Contains(searchString));
            }

            Staff = await entity
                .ToListAsync();
        }

        public String GetStaffName(int Id)
        {
            string Name = _context.StaffMembers.Find(Id).FirstName;
            Name += " ";
            Name += _context.StaffMembers.Find(Id).LastName;
            return Name;
        }

        public String GetRoleName(int Id)
        {
            return _context.Roles.Find(Id).Name;
        }
    }
}
