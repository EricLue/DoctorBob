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

namespace DoctorBob.UI.Pages.TimeModelManager
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
        public TimeModel TimeModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimeModel = await _context.TimeModels.FirstOrDefaultAsync(m => m.Id == id);

            if (TimeModel == null)
            {
                return NotFound();
            }

            historyTemp = TimeModel.History;

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
            TimeModel.Active = true;
            TimeModel.HistoryTemp = "⊕ " + modifyDateTime.DateTime + " - " + "eluechinger" + " / " + TimeModel.Time +
                " / " + TimeModel.DailyNumber;
            if (TimeModel.Active)
            {
                TimeModel.HistoryTemp += " - AKTIV";
            }
            else
            {
                TimeModel.HistoryTemp += " - INAKTIV";
            }
            TimeModel.HistoryTempInternal = ReadHistory();
            DeleteContent();

            // Anpassen auf CurrentUser
            TimeModel.ModifiedBy = "eluechinger";
            TimeModel.ModifiedAt = modifyDateTime.DateTime;

            _context.Attach(TimeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeModelExists(TimeModel.Id))
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

        private bool TimeModelExists(int id)
        {
            return _context.TimeModels.Any(e => e.Id == id);
        }
    }
}
