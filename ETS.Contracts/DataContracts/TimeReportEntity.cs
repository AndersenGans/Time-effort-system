using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETS.Contracts.DataContracts
{
    public class TimeReportEntity
    {
        [Key]
        public int TimeReportId { get; set; }
        public int TaskId { get; set; }
        public int AccountId { get; set; }
        public int ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public float Effort { get; set; }

        public float Overtime { get; set; }

        public string Description { get; set; }

        public ReportStatus Status { get; set; }

        public virtual AccountEntity Accounts { get; set; }

        public virtual ProjectEntity Projects { get; set; }

        //public virtual TaskEntity Tasks { get; set; }
    }
}
