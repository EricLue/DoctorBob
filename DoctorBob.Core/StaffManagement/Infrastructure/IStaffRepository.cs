using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorBob.Core.StaffManagement.Domain;
using DoctorBob.Core.Common.Infrastructure;

namespace DoctorBob.Core.StaffManagement.Infrastructure
{
    interface IStaffRepository : IRepository<Staff>
    {
        
    }
}
