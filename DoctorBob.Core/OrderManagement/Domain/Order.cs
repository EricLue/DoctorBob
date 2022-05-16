using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.RobotManagement.Domain;
using DoctorBob.Core.PatientManagement.Domain;
using DoctorBob.Core.Common.Domain;

namespace DoctorBob.Core.OrderManagement.Domain
{
    public class Order : AuditableEntity
    {
        public int Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RobotId { get; set; }
        public Robot Robot { get; set; }
        public List<Patient> Patients { get; set; }
        public State State { get; set; }
    }
}
