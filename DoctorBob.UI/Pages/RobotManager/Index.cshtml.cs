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
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Robot = _context.Robots.ToList();
        }

        public IList<Robot> Robot { get;set; }

        public async Task OnGetAsync()
        {
            Robot = await _context.Robots
                .Include(r => r.LastRoom).ToListAsync();
        }

        public String GetLastRoomName(int Id)
        {
            return _context.Rooms.Find(Id).Name;
        }

        public String GetCurrentLocationName(int Id)
        {
            return _context.CurrentLocations.Find(Id).Name;
        }

        public String GetActivityName(int Id)
        {
            return _context.Activities.Find(Id).Name;
        }
    }
}
