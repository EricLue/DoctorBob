using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.TherapyManagement.Domain;
using System.IO;

namespace DoctorBob.UI.Pages.TherapyManager
{
    public class EditModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;
        string historyTemp;

        public EditModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Therapy Therapy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Therapy = await _context.Therapies
                .Include(t => t.Drug)
                .Include(t => t.TimeModel).FirstOrDefaultAsync(m => m.Id == id);

            if (Therapy == null)
            {
                return NotFound();
            }
           ViewData["DrugId"] = new SelectList(_context.Drugs.Where(e => e.Active), "Id", "Id");
           ViewData["TimeModelId"] = new SelectList(_context.TimeModels.Where(e => e.Active), "Id", "Id");

            historyTemp = Therapy.History;

            if (!String.IsNullOrEmpty(historyTemp))
            {
                WriteHistory();

            }

            return Page();
        }

        public void WriteHistory()
        {
            // Set a variable to the Documents path.
            string docPath = @"C:\visual\DoctorBob\DoctorBob.Core\Common\Domain";

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "History.txt")))
            {
                outputFile.WriteLine(historyTemp);
            }
        }

        public String ReadHistory()
        {
            string readedHistory;
            // Set a variable to the Documents path.
            string docPath = @"C:\visual\DoctorBob\DoctorBob.Core\Common\Domain";

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamReader inputFile = new StreamReader(Path.Combine(docPath, "History.txt")))
            {
                readedHistory = inputFile.ReadToEnd();
            }
            return readedHistory;
        }

        public void DeleteContent()
        {
            // Set a variable to the Documents path.
            string docPath = @"C:\visual\DoctorBob\DoctorBob.Core\Common\Domain";

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "History.txt")))
            {
                outputFile.WriteLine("");
            }

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DateTimeOffset modifyDateTime = DateTimeOffset.Now;
            Therapy.Active = true;
            Therapy.HistoryTemp = "⊕ " + modifyDateTime.DateTime + " - " + "eluechinger" + " / " + Therapy.Name +
                " / " + Therapy.QuantityOfDrug + " / " +
                _context.Drugs.Find(Therapy.DrugId).Name + " / " +
                Therapy.TimeModelId + " / " + _context.IntakeCategories.Find(Therapy.IntakeCategoryId).Name +
                _context.StaffMembers.Find(Therapy.ResponsibleStaffId).LastName;
            if (Therapy.Active)
            {
                Therapy.HistoryTemp += " - AKTIV";
            }
            else
            {
                Therapy.HistoryTemp += " - INAKTIV";
            }
            Therapy.HistoryTempInternal = ReadHistory();
            DeleteContent();

            // Anpassen auf CurrentUser
            Therapy.ModifiedBy = "eluechinger";
            Therapy.ModifiedAt = modifyDateTime.DateTime;

            _context.Attach(Therapy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TherapyExists(Therapy.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TherapyExists(int id)
        {
            return _context.Therapies.Any(e => e.Id == id);
        }
    }
}
