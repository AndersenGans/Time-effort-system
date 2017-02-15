using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETS.Contracts.DataContracts
{
    public class AccountEntity
    {
        [Key]
        public int AccountId { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int AccessLevel { get; set; }

        public string NameSurname { get { return $"{Name} {Surname}"; } }

        public virtual ICollection<TimeReportEntity> TimeReports { get; set; }

        public virtual ICollection<Teammate> Teammates { get; set; }

    }
}
