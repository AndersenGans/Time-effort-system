using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class ComboProjectTimeReports
    {
        public List<TimeReportModel> TimeReport { get; set; }
        public List<ProjectManagementModel> Projects { get; set; }
        public string CurrentUser { get; set; }
    }
}