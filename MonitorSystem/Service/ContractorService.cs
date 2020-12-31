using Microsoft.EntityFrameworkCore;
using MonitorSystem.Data;
using MonitorSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSystem.Service
{
    public class ContractorService : IContractorService
    {
        public readonly ApplicationDbContext ApplicationDbContext;
        public readonly List<Contractor> Contractors;

        public ContractorService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
            Contractors = ApplicationDbContext.Contractors.ToList();
        }

        public void Dispose()
        {
            ApplicationDbContext.Dispose();

        }




        public Task<Contractor> GetByName(string name)
        {
            return ApplicationDbContext.Contractors.FirstOrDefaultAsync(a => a.Name.Equals(name, StringComparison.CurrentCulture));
        }



        public bool ValidName(string name)
        {
            return Contractors.All(a => a.Name != name);
        }


    }
}
