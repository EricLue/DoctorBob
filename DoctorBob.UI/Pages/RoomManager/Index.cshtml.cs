using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.UI.Pages.RoomManager
{
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Room = _context.Rooms.ToList();
        }

        public IList<Room> Room { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            var entity = from e in _context.Rooms
                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString.Contains("Aktiv") || searchString.Contains("aktiv"))
                {
                    entity = entity.Where(e => e.Active);
                }
                else
                {
                    entity = entity.Where(e => e.Name.Contains(searchString));
                }
            }
            Room = await entity.ToListAsync();
        }

        public int IsFree(int id)
        {
            if (id < 900)
            {
                bool found = false;
                foreach (var entity in _context.Patients.ToList())
                {
                    if (entity.Active)
                    {
                        if (entity.RoomId == id)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    // frei
                    return 1;
                }
                else
                {
                    // belegt
                    return 2;
                }
            }
            else
            {
                // kein Patientenraum
                return 3;
            }
        }
    }
}
