using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.OrderManagement.Domain;

namespace DoctorBob.UI.Pages.OrderManager
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
        ViewData["Robot"] = new SelectList(_context.Robots, "Id", "Name");
        ViewData["State"] = new SelectList(_context.States, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.State = _context.States.Find(1);

            // Anpassen auf CurrentUser
            Order.CreatedBy = "eluechinger";
            Order.CreatedAt = DateTimeOffset.UtcNow;

            // Anpassen auf CurrentUser
            Order.ModifiedBy = "eluechinger";
            Order.ModifiedAt = DateTimeOffset.UtcNow;

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
