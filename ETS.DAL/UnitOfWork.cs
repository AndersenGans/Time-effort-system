using System;
using ETS.Contracts.DataContracts;
using ETS.DAL.Database;
using System.Threading.Tasks;

namespace ETS.DAL
{
    public class UnitOfWork
    {
        private DatabaseContext db = new DatabaseContext();
        private GenericRepository<AccountEntity> accountRepository;
        private GenericRepository<ProjectEntity> projectRepository;
        private GenericRepository<RoleEntity> roleRepository;
        private GenericRepository<TaskEntity> taskRepository;
        private GenericRepository<TimeReportEntity> timeReportRepository;

        public GenericRepository<AccountEntity> AccountRepository
        {
            get
            {
                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<AccountEntity>(db);
                }
                return accountRepository;
            }
        }

        public GenericRepository<ProjectEntity> ProjectRepository
        {
            get
            {
                if (this.projectRepository == null)
                {
                    this.projectRepository = new GenericRepository<ProjectEntity>(db);
                }
                return projectRepository;
            }
        }

        public GenericRepository<RoleEntity> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new GenericRepository<RoleEntity>(db);
                }
                return roleRepository;
            }
        }

        public GenericRepository<TaskEntity> TaskRepository
        {
            get
            {
                if (this.taskRepository == null)
                {
                    this.taskRepository = new GenericRepository<TaskEntity>(db);
                }
                return taskRepository;
            }
        }

        public GenericRepository<TimeReportEntity> TimeReportRepository
        {
            get
            {
                if (this.timeReportRepository == null)
                {
                    this.timeReportRepository = new GenericRepository<TimeReportEntity>(db);
                }
                return timeReportRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
