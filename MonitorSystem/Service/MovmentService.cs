using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonitorSystem.Data;
using MonitorSystem.Entity;

namespace MonitorSystem.Service
{
    public class MonementService : IMovmentService
    {
        public readonly ApplicationDbContext ApplicationDbContext;

        public MonementService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public void Dispose()
        {
        }

      
    }
}
