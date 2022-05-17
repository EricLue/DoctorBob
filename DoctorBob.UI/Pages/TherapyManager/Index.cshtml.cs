﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.TherapyManagement.Domain;

namespace DoctorBob.UI.Pages.TherapyManager
{
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            Therapy = _context.Therapies.ToList();
        }

        public IList<Therapy> Therapy { get;set; }

        public async Task OnGetAsync()
        {
            Therapy = await _context.Therapies
                .Include(t => t.CaringStaff)
                .Include(t => t.Drug)
                .Include(t => t.TimeModel).ToListAsync();
        }

        public String GetDrugName(int Id)
        {
            return _context.Drugs.Find(Id).Name;
        }

        public String GetStaffName(int Id)
        {
            string Firstname = _context.StaffMembers.Find(Id).FirstName;
            string Lastname = _context.StaffMembers.Find(Id).LastName;
            return Firstname + " " + Lastname;
        }
    }
}