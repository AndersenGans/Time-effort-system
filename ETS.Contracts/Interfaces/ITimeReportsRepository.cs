using System.Collections.Generic;
using ETS.Contracts.DataContracts;

namespace ETS.Contracts.Interfaces
{
    public interface ITimeReportsRepository
    {
        IEnumerable<TimeReportEntity> GetAll();

        TimeReportEntity GetById(int id);

        void Add(TimeReportEntity report);

        void Update(TimeReportEntity reportWithChanges);

        void Delete(int id);
    }
}
