using System;
using System.Data.Entity;
using ETS.Contracts.DataContracts;

namespace ETS.DAL.Database
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            //into the table "Accounts"
            db.Accounts.Add(new AccountEntity
            {
                AccessLevel = 1,
                Email = "lemenvinks@yandex.ru",
                AccountId = 1,
                Login = "Bedlam",
                MiddleName = "Dmitriyevich",
                Name = "Oleg",
                Surname = "Kopot",
                Password = "111"
            });

            db.Accounts.Add(new AccountEntity
            {
                AccessLevel = 2,
                Email = "demienbad@yandex.ru",
                AccountId = 2,
                Login = "Grey",
                MiddleName = "Yuriyevich",
                Name = "Sergey",
                Surname = "Tereschenko",
                Password = "123"
            });

            db.Accounts.Add(new AccountEntity
            {
                AccessLevel = 3,
                Email = "127_los@yandex.ru",
                AccountId = 3,
                Login = "Dementor",
                MiddleName = "Mihaylovich",
                Name = "Artem",
                Surname = "Moiseenko",
                Password = "456"
            });

            //into the table "Projects"
            db.Projects.Add(new ProjectEntity
            {
                Description = "Some description about this.Project",
                ProjectId = 1,
                Name = "ETS"
            });

            db.Projects.Add(new ProjectEntity
            {
                Description = "Some description about this.Project",
                ProjectId = 2,
                Name = "TDO"
            });

            db.Projects.Add(new ProjectEntity
            {
                Description = "Some description about this.Project",
                ProjectId = 3,
                Name = "AppRes"
            });

            //into the table "Roles"
            db.Roles.Add(new RoleEntity
            {
                RoleId = 1,
                Name = "Engineer"
            });

            db.Roles.Add(new RoleEntity
            {
                RoleId = 2,
                Name = "Teamlead"
            });

            db.Roles.Add(new RoleEntity
            {
                RoleId = 3,
                Name = "Project manager"
            });

            //into the table "Tasks"
            db.Tasks.Add(new TaskEntity
            {
                Description = "Some description about this.Task",
                TaskId = 1,
                Title = "Development",
                ProjectId = 1 
            });

            db.Tasks.Add(new TaskEntity
            {
                Description = "Some description about this.Task",
                TaskId = 2,
                Title = "Testing",
                ProjectId = 2 
            });

            db.Tasks.Add(new TaskEntity
            {
                Description = "Some description about this.Task",
                TaskId = 3,
                Title = "Bug fixing",
                ProjectId = 3
            });

            //into the table "Teammate"
            db.Teammates.Add(new Teammate
            {
                AccountId = 1,
                ProjectId = 1,
                RoleId = 1
            });

            db.Teammates.Add(new Teammate
            {
                AccountId = 2,
                ProjectId = 2,
                RoleId = 2
            });

            db.Teammates.Add(new Teammate
            {
                AccountId = 3,
                ProjectId = 3,
                RoleId = 3
            });
            //into the table "TimeReports"
            db.TimeReports.Add(new TimeReportEntity
            {
                TimeReportId = 1,
                TaskId = 1,
                ProjectId = 1,
                AccountId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Effort = 5,
                Overtime = 2,
                Description = "Some description about this.TimeReport",
                Status = ReportStatus.Open
            });

            db.TimeReports.Add(new TimeReportEntity
            {
                TimeReportId = 2,
                TaskId = 2,
                ProjectId = 2,
                AccountId = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Effort = 4,
                Overtime = 3,
                Description = "Some description about this.TimeReport",
                Status = ReportStatus.Open
            });

            db.TimeReports.Add(new TimeReportEntity
            {
                TimeReportId = 3,
                TaskId = 3,
                ProjectId = 3,
                AccountId = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Effort = 6,
                Overtime = 2,
                Description = "Some description about this.TimeReport",
                Status = ReportStatus.Open
            });
        }
    }
}