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
        ViewData["Room"] = new SelectList(_context.Rooms.Where(e => e.Active), "Id", "Name");
        ViewData["Therapy"] = new SelectList(_context.Therapies.Where(e => e.Active), "Id", "Name");
        ViewData["CaringStaff"] = new SelectList(_context.StaffMembers.Where(e => e.Active)
            .Where(e => e.RoleId == 1 || e.RoleId == 4), "Id", "LastName");
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

            Patient.Active = true;
            Patient.History = "⊕ " + Patient.ModifiedAt.DateTime.AddHours(2) + " - eluechinger / " + Patient.FirstName.Substring(0, 1) + ". " + Patient.LastName +
                " / ";
            if (Patient.GenderId == 1)
            {
                Patient.History += " M / ";
            }
            if (Patient.GenderId == 2)
            {
                Patient.History += " F / ";
            }
            if (Patient.GenderId == 3)
            {
                Patient.History += " D / ";
            }

            Patient.History += " Raum " + Patient.RoomId + " / " +
                _context.Therapies.Find(Patient.TherapyId).Name + " / " +
                _context.StaffMembers.Find(Patient.CaringStaffId).LastName + " / " + Patient.MedicalHistory +
                " / IN: " + Patient.EntryDate;

            _context.Patients.Add(Patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
