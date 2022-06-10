using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.DrugManagement.Domain;

namespace DoctorBob.UI.Pages.DrugManager
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
            return Page();
        }

        [BindProperty]
        public Drug Drug { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Anpassen auf CurrentUser
            Drug.CreatedBy = "eluechinger";
            Drug.CreatedAt = DateTimeOffset.UtcNow;

            // Anpassen auf CurrentUser
            Drug.ModifiedBy = "eluechinger";
            Drug.ModifiedAt = DateTimeOffset.UtcNow;
            Drug.Active = true;

            Drug.History = Drug.ModifiedAt.DateTime.AddHours(2) +
                " - eluechinger / " + Drug.Name +
                " / " + Drug.DoseInMg + " / " +
                Drug.Description + " - AKTIV";
            _context.Drugs.Add(Drug);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
