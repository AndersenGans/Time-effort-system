using System.Collections.Generic;
using ETS.Contracts.DataContracts;

namespace ETS.Contracts.Interfaces
{
    public interface ITasksRepository
    {
        IEnumerable<TaskEntity> GetAll();

        TaskEntity GetById(int id);

        void Add(TaskEntity report);

        void Update(TaskEntity reportWithChanges);

        void Delete(int id);
    }
}
