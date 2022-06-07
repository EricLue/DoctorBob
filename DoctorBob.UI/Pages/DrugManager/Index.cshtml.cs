using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.Common.Infrastructure.Context;
using DoctorBob.Core.DrugManagement.Domain;

namespace DoctorBob.UI.Pages.DrugManager
{
    public class IndexModel : PageModel
    {
        private readonly DoctorBobContext _context;
        public bool Active;

        public IndexModel(DoctorBobContext context)
        {
            _context = context;
            Drug = _context.Drugs.ToList();
        }

        public IList<Drug> Drug { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            var entity = from e in _context.Drugs
                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString.Contains("Aktiv") || searchString.Contains("aktiv"))
                {
                    entity = entity.Where(e => e.Active);
                }
                else
                {
                    entity = entity.Where(e => e.Name.Contains(searchString));
                }
            }
            Drug = await entity.ToListAsync();
        }
    }
}
