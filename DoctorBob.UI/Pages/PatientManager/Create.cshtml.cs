using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.UI.Pages.PatientManager
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
        ViewData["Gender"] = new SelectList(_context.Genders, "Id", "Name");
        ViewData["Room"] = new SelectList(_context.Rooms, "Id", "Name");
        ViewData["Therapy"] = new SelectList(_context.Therapies, "Id", "Name");
        ViewData["CaringStaff"] = new SelectList(_context.StaffMembers, "Id", "FirstName", "LastName");
            return Page();
        }

        [BindProperty]
        public Patient Patient { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Patient.LeavingDate = new DateTime(1, 1, 1, 0, 0, 0);

            // Anpassen auf CurrentUser
            Patient.CreatedBy = "eluechinger";
            Patient.CreatedAt = DateTimeOffset.UtcNow;

            // Anpassen auf CurrentUser
            Patient.ModifiedBy = "eluechinger";
            Patient.ModifiedAt = DateTimeOffset.UtcNow;

            _context.Patients.Add(Patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
