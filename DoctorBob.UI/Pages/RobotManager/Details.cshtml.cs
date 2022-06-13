using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.RobotManagement.Domain;

namespace DoctorBob.UI.Pages.RobotManager
{
    public class DetailsModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DetailsModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public Robot Robot { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Robot = await _context.Robots
                .Include(r => r.LastRoom).FirstOrDefaultAsync(m => m.Id == id);

            if (Robot == null)
            {
                return NotFound();
            }
            return Page();
        }
        public String GetLocationName(int Id)
        {
            return _context.CurrentLocations.Find(Id).Name;
        }

        public String GetActivityName(int Id)
        {
            return _context.Activities.Find(Id).Name;
        }

        public String GetLastRoomName(int Id)
        {
            return _context.Rooms.Find(Id).Name;
        }
    }
}
