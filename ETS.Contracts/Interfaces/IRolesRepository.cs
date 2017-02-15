using System.Collections.Generic;
using ETS.Contracts.DataContracts;

namespace ETS.Contracts.Interfaces
{
    public interface IRolesRepository
    {
        IEnumerable<RoleEntity> GetAll();

        RoleEntity GetById(int id);

        void Add(RoleEntity role);

        void Update(RoleEntity roleWithChanges);

        void Delete(int id);
    }
}
