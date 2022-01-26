using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBob.Core.Common.Infrastructure
{
    interface IRepository<T>
    {
        // Get all Entities
        Task<IEnumerable<T>> Get();
        // Get single Entity by ID
        Task<T> Get(int id);
        // Create an Entity
        Task<T> Create(T entity);
        // Update Entity
        Task Update(T entity);
        // Delete Entity
        Task Delete(int id);
    }
}
