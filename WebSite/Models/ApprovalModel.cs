using System;
using ETS.Contracts.DataContracts;

namespace WebSite.Models
{
    public class ApprovalModel
    {
        public int Id { get; set; }

        public string TeamMember { get; set; }

        public string Project { get; set; }

        public string Task { get; set; }

        public double Effort { get; set; }

        public double Overtime { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ReportStatus Status { get; set; }
    }
}