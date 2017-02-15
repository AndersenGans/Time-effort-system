using System.Collections.Generic;
using ETS.Contracts.DataContracts;

namespace WebSite.Models
{
    public class ManageTimeReportModel
    {
        public int Id { get; set; }

        public ReportSpentHours SpentHours { get; set; }

        public int ProjectId { get; set; }

        public int TaskId { get; set; }

        public IEnumerable<ProjectEntity> Projects { get; set; }

        public IEnumerable<TaskEntity> Tasks { get; set; }

        public string Description { get; set; }

        public TimeReportInterval TimeInterval { get; set; }
    }
}