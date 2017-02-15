using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETS.Contracts.DataContracts
{
    public class ProjectEntity
    {
        [Key]
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Teammate> Teammates { get; set; }

        //public virtual ICollection<TimeReportEntity> TimeReports { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; } 
    }
}
