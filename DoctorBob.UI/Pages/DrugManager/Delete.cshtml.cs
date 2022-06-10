using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.DrugManagement.Domain;
using DoctorBob.Core.TherapyManagement.Domain;

namespace DoctorBob.UI.Pages.DrugManager
{
    public class DeleteModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DeleteModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Drug Drug { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drug = await _context.Drugs.FirstOrDefaultAsync(m => m.Id == id);

            if (Drug == null)
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

            Drug = await _context.Drugs.FindAsync(id);

            if (Drug != null)
            {
                bool used = false;
                List<Therapy> list = _context.Therapies.ToList<Therapy>();
                foreach (var entity in list)
                {
                    if (entity.DrugId == Drug.Id)
                    {
                        used = true;
                        break;
                    }
                }

                if (!used)
                {
                    Drug.Active = false;
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
