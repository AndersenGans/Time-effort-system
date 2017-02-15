using ETS.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Contracts.Interfaces
{
    public interface IRepository<T>
    {
        void Insert(T Element);

        void Delete(object id);

        void Update(T Element);

        Task<T> GetByID(object id);

        Task<List<T>> GetAll();

        //AccountEntity GetUser(string email);
    }
}
