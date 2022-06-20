using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.OrderManagement.Domain;

namespace DoctorBob.UI.Pages.OrderManager
{
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Order = _context.Orders.Include(x => x.OrderPatients).ThenInclude(x => x.Patient).ToList();
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            var entity = from e in _context.Orders
                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                entity = entity.Where(e => e.CreatedAt.ToString().Contains(searchString));
            }

            Order.OrderBy(x => x.Id);
            Order = await entity
                .Include(o => o.Robot)
                .Include(o => o.State).ToListAsync();
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

        public void RunRobot(int Id)
        {
            _context.Orders.Find(Id).StateId = 2;
            Refresh();
        }

        public IActionResult Refresh()
        {
            return new RedirectToPageResult("/OrderManager/Index");
        }
    }
}
