using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.StaffManagement.Domain;
using DoctorBob.Core.TherapyManagement.Domain;
using DoctorBob.Core.PatientManagement.Domain;
using System.IO;

namespace DoctorBob.UI.Pages.StaffManager
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
        public Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.StaffMembers.FirstOrDefaultAsync(m => m.Id == id);

            if (Staff == null)
            {
                return NotFound();
            }

            historyTemp = Staff.History;

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

            Staff = await _context.StaffMembers.FindAsync(id);

            if (Staff != null)
            {
                bool used = false;
                List<Therapy> list = _context.Therapies.ToList<Therapy>();
                foreach (var entity in list)
                {
                    if (entity.Active)
                    {
                        if (entity.ResponsibleStaffId == Staff.Id)
                        {
                            used = true;
                            break;
                        }
                    }
                }

                List<Patient> list1 = _context.Patients.ToList<Patient>();
                foreach (var entity in list1)
                {
                    if (entity.Active)
                    {
                        if (entity.CaringStaffId == Staff.Id)
                        {
                            used = true;
                            break;
                        }
                    }
                }

                if (!used)
                {
                    DateTimeOffset modifyDateTime = DateTimeOffset.Now;
                    Staff.Active = false;

                    Staff.HistoryTemp = "⊕ " + modifyDateTime.DateTime + " - " + "eluechinger" + " / " + Staff.FirstName + " " + Staff.LastName + " / " +
                _context.Roles.Find(Staff.RoleId).Name + " / " + Staff.Username + " / *** - INAKTIV";

                    Staff.HistoryTempInternal = ReadHistory();
                    DeleteContent();

                    // Anpassen auf CurrentUser
                    Staff.ModifiedBy = "eluechinger";
                    Staff.ModifiedAt = modifyDateTime.DateTime;
                    await _context.SaveChangesAsync();
                }
            }

                return RedirectToPage("./Index");
            }

        public String GetRoleName(int id)
        {
            return _context.Roles.Find(id).Name;
        }
    }
    }
