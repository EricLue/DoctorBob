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
        ViewData["CaringStaffId"] = new SelectList(_context.StaffMembers, "Id", "Id");
        ViewData["DrugId"] = new SelectList(_context.Drugs, "Id", "Id");
        ViewData["TimeModelId"] = new SelectList(_context.TimeModels, "Id", "Id");
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

            _context.Therapies.Add(Therapy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
