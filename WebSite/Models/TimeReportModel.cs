using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class TimeReportModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name can not be empty")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "TaskName can not be empty")]
        public string TaskName { get; set; }

        public ReportSpentHours SpentHours { get; set; }

        [Required(ErrorMessage = "Description of the task can not be empty")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "The length should be between 10 and 100 characters")]
        public string Description { get; set; }

        public string Status { get; set; }

        public TimeReportInterval TimeInterval { get; set; }

        public string CurrentUser { get; set; }
    }
}