using System;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class TimeReportInterval
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start date")]
        [Required(ErrorMessage = "Enter the date from which began the task")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Completion date")]
        [Required(ErrorMessage = "Enter the date when completed this task")]
        public DateTime EndDate { get; set; }
    }
}