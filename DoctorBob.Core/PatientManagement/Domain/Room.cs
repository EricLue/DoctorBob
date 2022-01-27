using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.Common.Domain;

namespace DoctorBob.Core.PatientManagement.Domain
{
    public class Room : AuditableEntity
    {
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
    }
}
