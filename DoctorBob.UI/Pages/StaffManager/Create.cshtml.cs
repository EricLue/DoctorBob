using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.StaffManagement.Domain;

namespace DoctorBob.UI.Pages.StaffManager
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
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Staff Staff { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Anpassen auf CurrentUser
            Staff.CreatedBy = "eluechinger";
            Staff.CreatedAt = DateTimeOffset.UtcNow;

            // Anpassen auf CurrentUser
            Staff.ModifiedBy = "eluechinger";
            Staff.ModifiedAt = DateTimeOffset.UtcNow;
            Staff.Active = true;


            Staff.History = "⊕ " + Staff.ModifiedAt.DateTime.AddHours(2) +
                " - eluechinger / " + Staff.FirstName + " " + Staff.LastName + " / " +
                _context.Roles.Find(Staff.RoleId).Name + " / " + Staff.Username + " / *** - AKTIV";
            _context.StaffMembers.Add(Staff);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
