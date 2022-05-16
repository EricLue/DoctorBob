using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.Common.Domain;

namespace DoctorBob.Core.TherapyManagement.Domain
{
    public class TimeModel : AuditableEntity
    {
        public int Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Time { get; set; }
        public int DailyNumber { get; set; }
    }
}
