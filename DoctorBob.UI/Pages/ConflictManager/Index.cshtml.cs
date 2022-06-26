using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.OrderManagement.Domain;
using DoctorBob.Core.RobotManagement.Domain;

namespace DoctorBob.UI.Pages.ConflictManager
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

            foreach (Order order in _context.Orders.ToList())
            {
                if (order.StateId == 1 || order.StateId == 2)
                {
                    order.StateId = 4;

                    Robot robot = _context.Robots.Find(order.RobotId);
                    robot.ActivityId = 10;

                    _context.Attach(robot).State = EntityState.Modified;
                    _context.Attach(order).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
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

        public String GetFailureName()
        {
            Random rd = new Random();
            int rand_num = rd.Next(1,10);

            if (rand_num == 1)
            {
                return "DBF-52 / Hub-Servo überlastet";
            }
            if (rand_num == 2)
            {
                return "DBF-81 / Fehler in Greif-Sensorik (obere Endlage)";
            }
            if (rand_num == 3)
            {
                return "DBF-9 / GoBBo-Kabelproblem";
            }
            if (rand_num == 5)
            {
                return "DBF-13 / Antriebsproblem";
            }
            if (rand_num == 6)
            {
                return "DBF-77 / Störung TOF-Sensor";
            }
            if (rand_num == 7)
            {
                return "DBF-3 / Connection lost Motorenboard";
            }
            if (rand_num == 8)
            {
                return "DBF-101 / Connection lost Main-Board";
            }
            if (rand_num == 9)
            {
                return "DBF-25 / Fehler in Greif-Sensorik (untere Endlage)";
            }
            else
            {
                return "DBF-39 / Überhitzung";
            }
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
