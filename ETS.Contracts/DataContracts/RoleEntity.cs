using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETS.Contracts.DataContracts
{
    public class RoleEntity
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Teammate> Teammates { get; set; }
    }
}
