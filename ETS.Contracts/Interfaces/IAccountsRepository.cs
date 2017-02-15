using System.Collections.Generic;
using ETS.Contracts.DataContracts;

namespace ETS.Contracts.Interfaces
{
    public interface IAccountsRepository
    {
        IEnumerable<AccountEntity> GetAll();

        AccountEntity GetById(int id);

        void Add(AccountEntity account);

        void Update(AccountEntity accountWithChanges);

        void Delete(int id);
    }
}
