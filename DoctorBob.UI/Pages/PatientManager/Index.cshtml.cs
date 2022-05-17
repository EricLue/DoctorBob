﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.UI.Pages.PatientManager
{
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Patient = _context.Patients.ToList();
        }

        public IList<Patient> Patient { get;set; }

        public async Task OnGetAsync()
        {
            Patient = await _context.Patients
                .Include(p => p.Gender)
                .Include(p => p.Room)
                .Include(p => p.Therapy).ToListAsync();
        }

        public String GetGenderName(int Id)
        {
            return _context.Genders.Find(Id).Name;
        }

        public String GetRoomName(int Id)
        {
            return _context.Rooms.Find(Id).Name;
        }

        public String GetTherapyName(int Id)
        {
            return _context.Therapies.Find(Id).Name;
        }
    }
}