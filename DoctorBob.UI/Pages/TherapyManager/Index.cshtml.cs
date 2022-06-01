using System;
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

        public async Task OnGetAsync(string searchString)
        {
            var entity = from e in _context.Therapies
                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                entity = entity.Where(e => e.Name.Contains(searchString) || e.Drug.Name.Contains(searchString));
            }

            Therapy = await entity
                .Include(t => t.Drug)
                .Include(t => t.TimeModel).ToListAsync();
        }

        public String GetDrugName(int Id)
        {
            return _context.Drugs.Find(Id).Name;
        }
    }
}
