using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBob.Core.Common.Domain
{
    public abstract class AuditableEntity
    {
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
