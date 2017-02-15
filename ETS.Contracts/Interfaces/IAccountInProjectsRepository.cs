using System.Collections.Generic;
using ETS.Contracts.DataContracts;

namespace ETS.Contracts.Interfaces
{
    public interface IAccountInProjectsRepository
    {
        IEnumerable<AccountInProjectEntity> GetAll();

        AccountInProjectEntity GetById(int id);

        void Add(AccountInProjectEntity role);

        void Update(AccountInProjectEntity roleWithChanges);

        void Delete(int id);
    }
}
