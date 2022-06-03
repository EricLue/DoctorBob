using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.OrderManagement.Domain;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.UI.Pages.OrderManager
{
    public class CreateModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;
        ICollection<Patient> patientList;

        public CreateModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
        }

        public void BindList() {
            patientList = _context.Patients.ToList();
        }

        public ICollection<Patient> GetList()
        {
            BindList();
            if (patientList.Count<=0)
            {
                return null;
            }
            else
            {
                return patientList;
            }
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

            //Order.OrderPatients = new List<OrderPatient>
            //{
            //    new OrderPatient
            //    {
            //        OrderId = Order.Id,
            //        PatientId = 10002
            //    },
            //    new OrderPatient
            //    {
            //        OrderId = Order.Id,
            //        PatientId = 10003
            //    },
            //    new OrderPatient
            //    {
            //        OrderId = Order.Id,
            //        PatientId = 10004
            //    },
            //    new OrderPatient
            //    {
            //        OrderId = Order.Id,
            //        PatientId = 10005
            //    }
            //};

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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

        public String GetPatientId(int Id)
        {
            return _context.Patients.Find(Id).Id.ToString();
        }
    }
}
