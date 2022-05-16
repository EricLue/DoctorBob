using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.Common.Domain;

namespace DoctorBob.Core.StaffManagement.Domain
{
    public class Staff : AuditableEntity
    {
        public int Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
