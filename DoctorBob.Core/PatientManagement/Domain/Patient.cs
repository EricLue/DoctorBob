using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.TherapyManagement.Domain;
using DoctorBob.Core.Common.Domain;
using DoctorBob.Core.OrderManagement.Domain;
using DoctorBob.Core.StaffManagement.Domain;

namespace DoctorBob.Core.PatientManagement.Domain
{
    public class Patient : AuditableEntity
    {
        public int Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int TherapyId { get; set; }
        public Therapy Therapy { get; set; }
        public string MedicalHistory { get; set; }
        public int CaringStaffId { get; set; }
        public Staff CaringStaff { get; set; }
        public ICollection<OrderPatient> OrderPatients { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime LeavingDate { get; set; }
    }
}
