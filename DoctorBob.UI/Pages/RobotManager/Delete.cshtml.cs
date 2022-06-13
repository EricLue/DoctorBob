using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.RobotManagement.Domain;
using DoctorBob.Core.OrderManagement.Domain;

namespace DoctorBob.UI.Pages.RobotManager
{
    public class DeleteModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DeleteModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Robot = await _context.Robots.FindAsync(id);

            if (Robot != null)
            {
                bool used = false;
                List<Order> list = _context.Orders.ToList<Order>();
                foreach (var entity in list)
                {
                    if (entity.RobotId == Robot.Id)
                    {
                        if (entity.StateId != 3)
                        {
                            used = true;
                        }
                    }
                }

                if (!used)
                {
                    DateTimeOffset modifyDateTime = DateTimeOffset.Now;
                    Robot.Active = false;

                    // Anpassen auf CurrentUser
                    Robot.ModifiedBy = "eluechinger";
                    Robot.ModifiedAt = modifyDateTime.DateTime;

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
