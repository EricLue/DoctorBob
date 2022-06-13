using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.TherapyManagement.Domain;

namespace DoctorBob.UI.Pages.TimeModelManager
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
        public TimeModel TimeModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Anpassen auf CurrentUser
            TimeModel.CreatedBy = "eluechinger";
            TimeModel.CreatedAt = DateTimeOffset.UtcNow;

            // Anpassen auf CurrentUser
            TimeModel.ModifiedBy = "eluechinger";
            TimeModel.ModifiedAt = DateTimeOffset.UtcNow;
            TimeModel.Active = true;

            TimeModel.History = "⊕ " + TimeModel.ModifiedAt.DateTime.AddHours(2) +
                " - eluechinger / " + TimeModel.Time +
                " / " + TimeModel.DailyNumber + " - AKTIV";

            _context.TimeModels.Add(TimeModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
