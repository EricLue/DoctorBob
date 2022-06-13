using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.TherapyManagement.Domain;
using DoctorBob.Core.PatientManagement.Domain;
using System.IO;

namespace DoctorBob.UI.Pages.TherapyManager
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Therapy = await _context.Therapies.FindAsync(id);

            if (Therapy != null)
            {
                bool used = false;
                List<Patient> list = _context.Patients.ToList<Patient>();
                foreach (var entity in list)
                {
                    if (entity.Active)
                    {
                        if (entity.TherapyId == Therapy.Id)
                        {
                            used = true;
                            break;
                        }
                    }
                }

                if (!used)
                {
                    DateTimeOffset modifyDateTime = DateTimeOffset.Now;
                    Therapy.Active = false;

                    Therapy.HistoryTemp = "⊕ " + modifyDateTime.DateTime + " - " + "eluechinger" + " / " + Therapy.Name +
                " / " + Therapy.QuantityOfDrug + " / " +
                _context.Drugs.Find(Therapy.DrugId).Name + " / " +
                Therapy.TimeModelId + " / " + _context.IntakeCategories.Find(Therapy.IntakeCategoryId).Name +
                _context.StaffMembers.Find(Therapy.ResponsibleStaffId).LastName + " - INAKTIV";

                    Therapy.HistoryTempInternal = ReadHistory();
                    DeleteContent();

                    // Anpassen auf CurrentUser
                    Therapy.ModifiedBy = "eluechinger";
                    Therapy.ModifiedAt = modifyDateTime.DateTime;
                    await _context.SaveChangesAsync();
                }
            }

                return RedirectToPage("./Index");
          
        }

        public String GetDrugName (int id)
        {
            return _context.Drugs.Find(id).Name;
        }
    }
}
