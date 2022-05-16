using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.Common.Domain;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.Core.RobotManagement.Domain
{
    public class Robot : AuditableEntity
    {
        public int Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
        public int LastRoomId { get; set; }
        public Room LastRoom { get; set; }
        public int CurrentLocationId { get; set; }
        public CurrentLocation CurrentLocation { get; set; }
        public int Power { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        // Warenkorb aka Auftrag verschiedene Patienten

    }
}
