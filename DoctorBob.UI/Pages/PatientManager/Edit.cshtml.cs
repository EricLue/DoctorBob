using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.PatientManagement.Domain;
using System.IO;

namespace DoctorBob.UI.Pages.PatientManager
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
        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient = await _context.Patients
                .Include(p => p.Room)
                .Include(p => p.Therapy)
                .Include(p => p.Gender)
                .Include(p => p.CaringStaff).FirstOrDefaultAsync(m => m.Id == id);

            if (Patient == null)
            {
                return NotFound();
            }

            historyTemp = Patient.History;

            if (!String.IsNullOrEmpty(historyTemp))
            {
                WriteHistory();

            }

            ViewData["Gender"] = new SelectList(_context.Genders, "Id", "Name");
            ViewData["Room"] = new SelectList(_context.Rooms.Where(e => e.Active), "Id", "Name");
            ViewData["Therapy"] = new SelectList(_context.Therapies.Where(e => e.Active), "Id", "Name");
            ViewData["CaringStaff"] = new SelectList(_context.StaffMembers.Where(e => e.Active)
                .Where(e => e.RoleId == 1 || e.RoleId == 4), "Id", "LastName");
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
            Patient.HistoryTemp = "⊕ " + modifyDateTime.DateTime + " - eluechinger / " + Patient.FirstName.Substring(0, 1) + ". " + Patient.LastName +
    " / ";
            if (Patient.GenderId == 1)
            {
                Patient.HistoryTemp += " M / ";
            }
            if (Patient.GenderId == 2)
            {
                Patient.HistoryTemp += " F / ";
            }
            if (Patient.GenderId == 3)
            {
                Patient.HistoryTemp += " D / ";
            }

            Patient.HistoryTemp += " Raum " + Patient.RoomId + " / " +
                _context.Therapies.Find(Patient.TherapyId).Name + " / " +
                _context.StaffMembers.Find(Patient.CaringStaffId).LastName + " / " + Patient.MedicalHistory +
                " / IN: " + Patient.EntryDate;

            if (Patient.LeavingDate == new DateTime(1, 1, 1, 0, 0, 0))
            {
                Patient.Active = true;
            }
            else
            {
                Patient.Active = false;
                string leavingDate = Patient.LeavingDate.ToString() + "";
                Patient.HistoryTemp += " / OUT: " + leavingDate;
            }

            Patient.HistoryTempInternal = ReadHistory();
            DeleteContent();

            // Anpassen auf CurrentUser
            Patient.ModifiedBy = "eluechinger";
            Patient.ModifiedAt = modifyDateTime.DateTime;

            _context.Attach(Patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(Patient.Id))
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

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
