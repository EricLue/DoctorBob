using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.TherapyManagement.Domain;

namespace DoctorBob.UI.Pages.TherapyManager
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
        ViewData["Drug"] = new SelectList(_context.Drugs.Where(e => e.Active), "Id", "Name");
        ViewData["TimeModel"] = new SelectList(_context.TimeModels.Where(e => e.Active), "Id", "Time");
            return Page();
        }

        [BindProperty]
        public Therapy Therapy { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Anpassen auf CurrentUser
            Therapy.CreatedBy = "eluechinger";
            Therapy.CreatedAt = DateTimeOffset.UtcNow;

            // Anpassen auf CurrentUser
            Therapy.ModifiedBy = "eluechinger";
            Therapy.ModifiedAt = DateTimeOffset.UtcNow;

            _context.Therapies.Add(Therapy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
