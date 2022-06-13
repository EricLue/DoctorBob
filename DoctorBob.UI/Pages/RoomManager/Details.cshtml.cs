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
    public class DetailsModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DetailsModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public Room Room { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
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
