using System;
using System.Collections.Generic;
using System.Linq;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;
using ETS.DAL.Database;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ETS.DAL
{
    public class TimeReportsInMemoryRepository : IRepository<TimeReportEntity>
    {
        private DatabaseContext db = new DatabaseContext();

        public async Task<List<TimeReportEntity>> GetAll()
        {
            return await db.TimeReports.ToListAsync();
        }

        public async Task<TimeReportEntity> GetByID(object id)
        {
            return await db.TimeReports.FirstOrDefaultAsync(timeReport => timeReport.TimeReportId == int.Parse(id.ToString()));
        }

        public void Insert(TimeReportEntity report)
        {
            report.TimeReportId = db.TimeReports.Count() + 1;
            db.TimeReports.Add(report);
            db.SaveChanges();
        }

        public void Update(TimeReportEntity reportWithChanges)
        {
            var reportToUpdate = db.TimeReports.First(rp => rp.TimeReportId == reportWithChanges.TimeReportId);
            reportToUpdate.Effort = reportWithChanges.Effort;
            reportToUpdate.Overtime = reportWithChanges.Overtime;
            reportToUpdate.StartDate = reportWithChanges.StartDate;
            reportToUpdate.EndDate = reportWithChanges.EndDate;
            reportToUpdate.Description = reportWithChanges.Description;
            reportToUpdate.Status = reportWithChanges.Status;
            reportToUpdate.TaskId = reportWithChanges.TaskId;
            reportToUpdate.ProjectId = reportWithChanges.ProjectId;
            reportToUpdate.AccountId = reportWithChanges.AccountId;
            db.SaveChanges();
        }

        public void Delete(object id)
        {
            db.TimeReports.Remove(db.TimeReports.Find(id));
            db.SaveChanges();
        }
    }
}
