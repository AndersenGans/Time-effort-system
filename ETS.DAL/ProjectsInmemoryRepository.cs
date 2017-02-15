using System;
using System.Collections.Generic;
using System.Linq;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;
using ETS.DAL.Database;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ETS.DAL
{
    public class ProjectsInmemoryRepository : IRepository<ProjectEntity>
    {
        private readonly DatabaseContext db = new DatabaseContext();


        public async Task<List<ProjectEntity>> GetAll()
        {
            return await db.Projects.ToListAsync();
        }

        public async Task<ProjectEntity> GetByID(object id)
        {
            return await db.Projects.FirstOrDefaultAsync(p => p.ProjectId == int.Parse(id.ToString()));
        }

        public void Insert(ProjectEntity report)
        {
            db.Projects.Add(report);
            db.SaveChanges();
        }

        public void Update(ProjectEntity projectWithChanges)
        {
            var projectToUpdate = db.Projects.First(rp => rp.ProjectId == projectWithChanges.ProjectId);

            projectToUpdate.ProjectId = projectWithChanges.ProjectId;
            projectToUpdate.Name = projectWithChanges.Name;
            projectToUpdate.Description = projectWithChanges.Description;

            if (projectWithChanges.Tasks != null)
            {
                foreach (var task in projectWithChanges.Tasks)
                {
                    projectToUpdate.Tasks.Add(task);
                }
            }

            if (projectWithChanges.Teammates != null)
            {
                foreach (var teammate in projectWithChanges.Teammates)
                {
                    projectToUpdate.Teammates.Add(teammate);
                }
            }

            foreach (var task in projectWithChanges.Tasks)
            {
                projectToUpdate.Tasks.Add(task);
            }

            foreach (var teammate in projectWithChanges.Teammates)
            {
                projectToUpdate.Teammates.Add(teammate);
            }

            db.SaveChanges();
        }

        public void Delete(object id)
        {
            db.Projects.Remove(db.Projects.Find(id));
            db.SaveChanges();
        }
    }
}
