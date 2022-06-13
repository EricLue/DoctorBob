using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.TherapyManagement.Domain;

namespace DoctorBob.UI.Pages.TimeModelManager
{
    public class IndexModel : PageModel
    {
        private readonly DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext _context;

        public IndexModel(DoctorBob.Core.Common.Infrastructure.Context.DoctorBobContext context)
        {
            _context = context;
            TimeModel = _context.TimeModels.ToList();
        }

        public IList<TimeModel> TimeModel { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            var entity = from e in _context.TimeModels
                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString.Contains("Aktiv") || searchString.Contains("aktiv"))
                {
                    entity = entity.Where(e => e.Active);
                }
                else
                {
                    entity = entity.Where(e => e.Time.Contains(searchString));
                }
            }

            TimeModel = await entity.ToListAsync();
        }
    }
}
