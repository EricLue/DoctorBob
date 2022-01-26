using DoctorBob.Core.StaffManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;

namespace DoctorBob.Core.StaffManagement.Infrastructure
{
    class StaffRepository : IStaffRepository
    {
        private readonly DoctorBobContext _context;

        public StaffRepository(DoctorBobContext context)
        {
            _context = context;
        }

        public async Task<Staff> Create(Staff entity)
        {
            _context.StaffMembers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var staffMemberToDelete = await _context.StaffMembers.FindAsync(id);
            _context.StaffMembers.Remove(staffMemberToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Staff>> Get()
        {
            return await _context.StaffMembers.ToListAsync();
        }

        public async Task<Staff> Get(int id)
        {
            return await _context.StaffMembers.FindAsync(id);
        }

        public async Task Update(Staff entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
