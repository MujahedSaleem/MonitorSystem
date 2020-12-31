using Microsoft.EntityFrameworkCore;
using MonitorSystem.Data;
using MonitorSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSystem.Service
{
    public class ProjectService : IProjectService
    {
        public readonly ApplicationDbContext ApplicationDbContext;
        public readonly List<Project> Projects;

        public ProjectService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
            Projects = ApplicationDbContext.Projects.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            ApplicationDbContext.Dispose();
        }



        public Task<Project> GetByName(string name)
        {
            return ApplicationDbContext.Projects.AsNoTracking().FirstOrDefaultAsync(a => a.ProjectName.Equals(name, StringComparison.CurrentCulture));
        }

        public bool ValidName(string name)
        {
            return Projects.Any(a => a.ProjectName == name);
        }


    }
}
