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
using DoctorBob.Core.TherapyManagement.Domain;
using DoctorBob.Core.API;
using System.IO;

namespace DoctorBob.UI.Pages.OrderManager
{
    public class ConfirmModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public ConfirmModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(p => p.OrderPatients)
                .Include(p => p.Robot).FirstOrDefaultAsync(m => m.Id == id);

            if (Order == null)
            {
                return NotFound();
            }

            List<String> commandsList = new List<String>();
            foreach (var orderPatient in Order.OrderPatients)
            {
                Patient patient = _context.Patients.Find(orderPatient.PatientId);
                string roomId = _context.Rooms.Find(patient.RoomId).Id.ToString();
                string command1 = "Raum/" + roomId + "/Patient/" + patient.Id;
                //_mqttClient.PublishAsync("Raum/101/Patient/", patientId);
                commandsList.Add(command1);

                Therapy therapy = _context.Therapies.Find(patient.TherapyId);
                string drugId = _context.Drugs.Find(therapy.DrugId).Id.ToString();
                string command2 = "Raum/" + roomId + "/Medikament/" + drugId;
                //_mqttClient.PublishAsync("Raum/101/Medikament/", drugId);
                commandsList.Add(command2);
            }

            MQTTClient.Main(commandsList);
            Order.StateId = 2;
            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Page();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        public String GetRobotName(int id)
        {
            return _context.Robots.Find(id).Name;
        }
    }
}
