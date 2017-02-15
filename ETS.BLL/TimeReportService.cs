using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETS.BLL.Infrastructure;
using ETS.Contracts.DataContracts;
using ETS.DAL;

namespace ETS.BLL
{
    public class TimeReportService/* : IRepository<TimeReportEntity>*/
    {
        private UnitOfWork unitOfWork;

        public TimeReportService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<TimeReportEntity>> GetAll()
        {
            return await unitOfWork.TimeReportRepository.GetAll();
        }

        public async Task<TimeReportEntity> GetByID(object id)
        {
            return await unitOfWork.TimeReportRepository.GetByID(id);
        }

        public async System.Threading.Tasks.Task Insert(TimeReportEntity report)
        {
            var a = await unitOfWork.TimeReportRepository.GetAll();
            if (report.TimeReportId == 0) report.TimeReportId = a.Last().TimeReportId + 1;
            if (await unitOfWork.TimeReportRepository.GetByID(report.TimeReportId) != null) throw new ValidationException("Duplicated ID found", "id");
            ReportValidate(report);
            unitOfWork.TimeReportRepository.Insert(report);
        }

        public async System.Threading.Tasks.Task Update(TimeReportEntity reportWithChanges)
        {
            var reportInDb = await unitOfWork.TimeReportRepository.GetByID(reportWithChanges.TimeReportId);
            if (reportInDb == null) throw new ValidationException("ID not found", "id");
            if(reportInDb.Status != ReportStatus.Open) throw new ValidationException("Could not edit report which is not opened","Status");
            ReportValidate(reportWithChanges);
            reportInDb.Description = reportWithChanges.Description;
            reportInDb.StartDate = reportWithChanges.StartDate;
            reportInDb.EndDate = reportWithChanges.EndDate;
            reportInDb.Accounts = reportWithChanges.Accounts;
            reportInDb.Effort = reportWithChanges.Effort;
            reportInDb.Overtime = reportWithChanges.Overtime;
            reportInDb.ProjectId = reportWithChanges.ProjectId;
            reportInDb.Projects = reportWithChanges.Projects;
            reportInDb.TaskId = reportWithChanges.TaskId;
            reportInDb.Status = reportWithChanges.Status;
            unitOfWork.Save();
            //unitOfWork.TimeReportRepository.Update(reportWithChanges);
        }

        public async System.Threading.Tasks.Task Delete(object id)
        {
            if (await unitOfWork.TimeReportRepository.GetByID(id) != null) unitOfWork.TimeReportRepository.Delete(id);
            else throw new ValidationException("Report with this id is not exist", "id");
        }

        private void ReportValidate(TimeReportEntity report)
        {
            if (report.EndDate.Date > DateTime.Today) throw new ValidationException("End date maximum value is today", "EndDate");
            if (report.EndDate < report.StartDate) throw new ValidationException("Start date can't be later then end date", "StartDate");
            if (report.Effort <= 0) throw new ValidationException("Effort can't be equal to zero or below zero", "Effort");
            if (report.Effort > 8*((report.EndDate - report.StartDate).Days + 1)) throw new ValidationException("Effort can't be bigger then 8 hrs/day", "Effort");
            if (report.Overtime < 0) throw new ValidationException("Overtime can't be below zero", "Overtime");
            if (report.Effort < 8 && report.Overtime > 0) throw new ValidationException("Overtime can't be above zero if Effort is below eight", "Overtime");
            //Project, Task are allways correct because of TimeReportsView. Description can be empty 
        }

        public async System.Threading.Tasks.Task Accept(int id)
        {
            if (await unitOfWork.TimeReportRepository.GetByID(id) == null) throw new ValidationException("ID not found", "id");
            var report = await unitOfWork.TimeReportRepository.GetByID(id);
            report.Status = ReportStatus.Accepted;
            unitOfWork.Save();
        }

        public async System.Threading.Tasks.Task Decline(int id)
        {
            if (await unitOfWork.TimeReportRepository.GetByID(id) == null) throw new ValidationException("ID not found", "id");
            var report = await unitOfWork.TimeReportRepository.GetByID(id);
            report.Status = ReportStatus.Declined;
            unitOfWork.Save();
        }

        public async System.Threading.Tasks.Task Notify(int id)
        {
            if (await unitOfWork.TimeReportRepository.GetByID(id) == null) throw new ValidationException("ID not found", "id");
            var report = await unitOfWork.TimeReportRepository.GetByID(id);
            report.Status = ReportStatus.Notified;
            unitOfWork.Save();
        }
    }
}
