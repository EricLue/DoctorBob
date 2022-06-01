using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.DrugManagement.Domain;
using DoctorBob.Core.StaffManagement.Domain;
using DoctorBob.Core.Common.Domain;

namespace DoctorBob.Core.TherapyManagement.Domain
{
    public class Therapy : AuditableEntity
    {
        public int Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
        public int QuantityOfDrug { get; set; }
        public int DrugId { get; set; }
        public Drug Drug { get; set; }
        //public int CaringStaffId { get; set; }
        //public Staff CaringStaff { get; set; }
        public int TimeModelId { get; set; }
        public TimeModel TimeModel { get; set; }
    }
}
