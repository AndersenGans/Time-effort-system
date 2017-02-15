using System.Collections.Generic;
using System.Linq;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;
using ETS.DAL.Database;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ETS.DAL
{
    public class RolesInMemoryRepository : IRepository<RoleEntity>
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public async Task<List<RoleEntity>> GetAll()
        {
            return await db.Roles.ToListAsync();
        }

        public async Task<RoleEntity> GetByID(object id)
        {
            return await db.Roles.FirstOrDefaultAsync(t => t.RoleId == int.Parse(id.ToString()));
        }

        public void Insert(RoleEntity role)
        {
            throw new System.NotImplementedException();
        }

        public void Update(RoleEntity roleWithChanges)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}
