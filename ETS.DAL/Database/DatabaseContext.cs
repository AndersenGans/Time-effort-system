using System.Data.Entity;
using ETS.Contracts.DataContracts;

namespace ETS.DAL.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("ETS_DB")
        { }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Teammate> Teammates { get; set; }
        public DbSet<TimeReportEntity> TimeReports { get; set; }
    }
}
