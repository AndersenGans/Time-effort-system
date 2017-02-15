using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETS.Contracts.Interfaces;
using ETS.Contracts.DataContracts;

namespace ETS.DAL
{
    class TimeReportsInDatabase : IRepository<TimeReportEntity>
    {
        public void Add(TimeReportEntity Element)
        {
            throw new NotImplementedException();
        }

        public void Delete(TimeReportEntity Element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeReportEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TimeReportEntity GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Update(TimeReportEntity Element)
        {
            throw new NotImplementedException();
        }
    }
}
