using System.Collections.Generic;
using System.Linq;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;
using ETS.DAL.Database;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ETS.DAL
{
    public class AccountsInMemoryRepository : IRepository<AccountEntity>
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public async Task<List<AccountEntity>> GetAll()
        {
            return await db.Accounts.ToListAsync();
        }

        public async Task<AccountEntity> GetByID(object id)
        {
            return await db.Accounts.FirstOrDefaultAsync(t => t.AccountId == int.Parse(id.ToString()));
        }

        public void Insert(AccountEntity account)
        {
            db.Accounts.Add(account);
        }

        public void Update(AccountEntity accountWithChanges)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new System.NotImplementedException();
        }        

        public bool Login(string login, string password)
        {
            var account = db.Accounts.FirstOrDefault(p => p.Login == login && p.Password == password);
            for (int i = 0; i< login.Length; i++)
            {
                if (login[i] != account.Login[i])
                {
                    return false;
                }
            }
            if (account != null)
            {
                return true;
            }
            return false;
        }       
    }
}
