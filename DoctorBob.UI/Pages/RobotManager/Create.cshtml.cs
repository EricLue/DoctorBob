using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.RobotManagement.Domain;

namespace DoctorBob.UI.Pages.RobotManager
{
    public class CreateModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public CreateModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["LastRoomId"] = new SelectList(_context.Rooms.Where(e => e.Active), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Robot Robot { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Robot.LastRoom = _context.Rooms.Find(999);
            Robot.CurrentLocation = _context.CurrentLocations.Find(999);
            Robot.Power = 100;
            Robot.Activity = _context.Activities.Find(1);

            // Anpassen auf CurrentUser
            Robot.CreatedBy = "eluechinger";
            Robot.CreatedAt = DateTimeOffset.UtcNow;

            // Anpassen auf CurrentUser
            Robot.ModifiedBy = "eluechinger";
            Robot.ModifiedAt = DateTimeOffset.UtcNow;

            _context.Robots.Add(Robot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
