using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETS.Contracts.DataContracts
{
    public class TaskEntity
    {
        [Key]
        public int TaskId { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public virtual ICollection<TimeReportEntity> TimeReports { get; set; }

        //public virtual ProjectEntity Projects { get; set; }
    }
}
