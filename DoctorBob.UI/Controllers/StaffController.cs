using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorBob.Core.StaffManagement.Infrastructure;
using DoctorBob.Core.StaffManagement.Domain;

namespace DoctorBob.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Staff>> GetStaff()
        {
            return await _staffRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            return await _staffRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff([FromBody] Staff staff)
        {
            var newStaff = await _staffRepository.Create(staff);
            return CreatedAtAction(nameof(GetStaff), new { id = newStaff.Id }, newStaff);
        }

        [HttpPut]
        public async Task<ActionResult> PutStaff(int id, [FromBody] Staff staff)
        {
            if (id != staff.Id)
            {
                return BadRequest();
            }

            await _staffRepository.Update(staff);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var staffToDelete = await _staffRepository.Get(id);
            if (staffToDelete == null)
            {
                return NotFound();
            }
            await _staffRepository.Delete(staffToDelete.Id);
            return NoContent();
        }
    }
}
