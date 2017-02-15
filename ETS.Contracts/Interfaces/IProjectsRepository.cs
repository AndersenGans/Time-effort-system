using System.Collections.Generic;
using ETS.Contracts.DataContracts;

namespace ETS.Contracts.Interfaces
{
    public interface IProjectsRepository
    {
        IEnumerable<ProjectEntity> GetAll();

        ProjectEntity GetById(int id);

        void Add(ProjectEntity report);

        void Update(ProjectEntity reportWithChanges);

        void Delete(int id);
    }
}
