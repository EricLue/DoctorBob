using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.Core.OrderManagement.Domain
{
    public class OrderPatient
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
