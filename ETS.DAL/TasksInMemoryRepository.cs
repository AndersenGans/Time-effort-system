using System.Collections.Generic;
using System.Linq;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;
using ETS.DAL.Database;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ETS.DAL
{
    public class TasksInMemoryRepository : IRepository<TaskEntity>
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public async Task<List<TaskEntity>> GetAll()
        {
            return await db.Tasks.ToListAsync();
        }

        public async Task<TaskEntity> GetByID(object id)
        {
            return await db.Tasks.FirstOrDefaultAsync(t => t.TaskId == int.Parse(id.ToString()));
        }

        public void Insert(TaskEntity report)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TaskEntity reportWithChanges)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}
