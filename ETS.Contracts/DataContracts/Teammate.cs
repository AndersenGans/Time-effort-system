using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETS.Contracts.DataContracts
{
    public class Teammate
    {
        [Key, Column(Order = 0)]
        public int AccountId { get; set; }

        [Key, Column(Order = 1)]
        public int ProjectId { get; set; }

        public int RoleId { get; set; }

        public virtual AccountEntity Account { get; set; }

        public virtual ProjectEntity Project { get; set; }

        //public virtual ICollection<TimeReportEntity> TimeReport { get; set; }

        //[ForeignKey("RoleId")]
        public virtual RoleEntity Role { get; set; }
    }
}