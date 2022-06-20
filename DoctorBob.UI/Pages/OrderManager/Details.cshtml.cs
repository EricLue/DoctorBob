using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.OrderManagement.Domain;
using DoctorBob.Core.PatientManagement.Domain;
using DoctorBob.Core.DrugManagement.Domain;
using DoctorBob.Core.TherapyManagement.Domain;

namespace DoctorBob.UI.Pages.OrderManager
{
    public class DetailsModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public DetailsModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.Robot)
                .Include(o => o.OrderPatients).ThenInclude(o => o.Patient).FirstOrDefaultAsync(o => o.Id == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        public String GetRobotName(int Id)
        {
            return _context.Robots.Find(Id).Name;
        }

        public String GetStateName(int Id)
        {
            return _context.States.Find(Id).Name;
        }

        public String GetPatient(int Id)
        {
            String patientInfo = _context.Patients.Find(Id).Id.ToString();
            patientInfo += " - ";
            patientInfo += _context.Patients.Find(Id).FirstName;
            patientInfo += " ";
            patientInfo += _context.Patients.Find(Id).LastName;
            return patientInfo;
        }

        public String GetPatientRoom(int Id)
        {
            Patient patient = _context.Patients.Find(Id);
            String roomNumber = _context.Rooms.Find(patient.RoomId).Id.ToString();
            return roomNumber;
        }

        public String GetPatientDrug(int Id)
        {
            Patient patient = _context.Patients.Find(Id);
            Therapy therapy = _context.Therapies.Find(patient.TherapyId);
            Drug drug = _context.Drugs.Find(therapy.DrugId);
            return drug.Name;
        }
    }
}
