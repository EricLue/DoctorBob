using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.DrugManagement.Domain;
using DoctorBob.Core.TherapyManagement.Domain;
using System.IO;

namespace DoctorBob.UI.Pages.DrugManager
{
    public class DeleteModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;
        string historyTemp;

        public DeleteModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Drug Drug { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drug = await _context.Drugs.FirstOrDefaultAsync(m => m.Id == id);

            if (Drug == null)
            {
                return NotFound();
            }

            historyTemp = Drug.History;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drug = await _context.Drugs.FindAsync(id);

            if (Drug != null)
            {
                bool used = false;
                List<Therapy> list = _context.Therapies.ToList<Therapy>();
                foreach (var entity in list)
                {
                    if (entity.Active)
                    {
                        if (entity.DrugId == Drug.Id)
                        {
                            used = true;
                            break;
                        }
                    }
                }

                if (!used)
                {
                    DateTimeOffset modifyDateTime = DateTimeOffset.Now;
                    Drug.Active = false;

                    Drug.HistoryTemp = "⊕ " + modifyDateTime.DateTime + " - " + "eluechinger" + " / " + Drug.Name +
                    " / " + Drug.DoseInMg +
                    " / " + Drug.Description + 
                    " - INAKTIV";

                    Drug.HistoryTempInternal = ReadHistory();
                    DeleteContent();

                    // Anpassen auf CurrentUser
                    Drug.ModifiedBy = "eluechinger";
                    Drug.ModifiedAt = modifyDateTime.DateTime;

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
