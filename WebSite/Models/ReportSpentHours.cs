using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class ReportSpentHours
    {
        [Required(ErrorMessage = "Enter the number of hours worked")]
        public float Effort { get; set; }

        [Required(ErrorMessage = "Enter the number of hours worked supratemporality")]
        public float Overtime { get; set; }
    }
}