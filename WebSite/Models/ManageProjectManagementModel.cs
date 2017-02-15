using System.Collections.Generic;
using ETS.Contracts.DataContracts;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class ManageProjectManagementModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Project name can not be empty")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "The length should be between 2 and 50 characters")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Description of the project can not be empty")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "The length should be between 10 and 100 characters")]
        public string Description { get; set; }

        public IEnumerable<TaskEntity> Task { get; set; }

        public IEnumerable<TaskEntity> TasksInProject { get; set; }

        public IEnumerable<AccountEntity> TeamMember { get; set; }

        public IEnumerable<RoleEntity> Role { get; set; }

        public IEnumerable<Teammate> TeammatesInProject { get; set; }
    }
}