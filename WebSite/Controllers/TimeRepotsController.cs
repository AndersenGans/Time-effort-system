using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ETS.BLL;
using ETS.Contracts.DataContracts;
using ETS.DAL;
using WebSite.Models;
using System.Threading.Tasks;

namespace WebSite.Controllers
{
    [Authorize]
    public class TimeReportsController : Controller
    {
        private UnitOfWork unitOfWork;
        private readonly TimeReportService reportsService;
        public static string dateStart = "", dateEnd = "";

        public TimeReportsController()
        {
            this.unitOfWork = new UnitOfWork();
            this.reportsService = new TimeReportService(unitOfWork);
        }

        //Зарезервированные задачи на проектах
        private readonly IDictionary<int, string> allTasks = new Dictionary<int, string>()
        {
            { 1, "Development" },
            { 2, "Testing" },
            { 3, "Bug fixing" }
        };

        // GET: TimeReports
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<TimeReportEntity> allReports = await this.reportsService.GetAll();
            var users = await unitOfWork.AccountRepository.GetAll();
            var projects = await unitOfWork.ProjectRepository.GetAll();
            List<ProjectManagementModel> a = new List<ProjectManagementModel>();
            foreach(var item in projects)
            {
                a.Add(new ProjectManagementModel()
                {
                    ProjectName = item.Name,
                    Id = item.ProjectId
                });
            }
            List<TimeReportModel> b = new List<TimeReportModel>();
            foreach (var item in allReports)
            {
                b.Add(new TimeReportModel
                {
                    Id = item.TimeReportId,
                    ProjectName = item.Projects.Name,
                    SpentHours = new ReportSpentHours
                    {
                        Effort = item.Effort,
                        Overtime = item.Overtime
                    },
                    TimeInterval = new TimeReportInterval
                    {
                        StartDate = item.StartDate,
                        EndDate = item.EndDate
                    },
                    CurrentUser = users.FirstOrDefault(m => m.Login == User.Identity.Name).NameSurname,
                    Status = item.Status.ToString(),
                    //ProjectName = reportEntity.Projects?.Name ?? "Нет доступа к экземпляру",
                    TaskName = item.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == item.TaskId)?.Title ?? "Нет доступа",
                    Description = item.Description
                });
            };
            List<ComboProjectTimeReports> reports = new List<ComboProjectTimeReports>();
            ComboProjectTimeReports reportsModel = new ComboProjectTimeReports()
            {
                Projects = a,
                TimeReport = b,
                CurrentUser = users.FirstOrDefault(m => m.Login == User.Identity.Name).NameSurname
            };
            reports.Add(reportsModel);
            return View(reports);
        }

        public async Task<PartialViewResult> FilterProjectName(int id)
        {
            if (id != -1)
            { //One project
                IEnumerable<TimeReportEntity> allTasks = await unitOfWork.TimeReportRepository.GetAll();

                IList<TimeReportModel> tasksModel = new List<TimeReportModel>();

                foreach (TimeReportEntity task in allTasks)
                {
                    if (task.ProjectId == id)
                    {
                        tasksModel.Add(new TimeReportModel()
                        {
                            Id = task.TimeReportId,
                            SpentHours = new ReportSpentHours
                            {
                                Effort = task.Effort,
                                Overtime = task.Overtime
                            },
                            TimeInterval = new TimeReportInterval
                            {
                                StartDate = task.StartDate,
                                EndDate = task.EndDate
                            },
                            ProjectName = task.Projects.Name,
                            TaskName = task.Projects.Tasks.FirstOrDefault(item => item.TaskId == task.TaskId)?.Title ?? "Нет доступа",
                            Status = task.Status.ToString(),
                            Description = task.Description,
                        });
                    }
                }

                return this.PartialView("TimeReportsFilters", tasksModel);
            }
            else
            { //All projects
                IEnumerable<TimeReportEntity> allReports = await this.reportsService.GetAll();

                IEnumerable<TimeReportModel> reportsModel = allReports.Select(reportEntity => new TimeReportModel
                {
                    Id = reportEntity.TimeReportId,
                    SpentHours = new ReportSpentHours
                    {
                        Effort = reportEntity.Effort,
                        Overtime = reportEntity.Overtime
                    },
                    TimeInterval = new TimeReportInterval
                    {
                        StartDate = reportEntity.StartDate,
                        EndDate = reportEntity.EndDate
                    },
                    ProjectName = reportEntity.Projects?.Name ?? "Нет доступа к экземпляру",
                    TaskName = reportEntity.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == reportEntity.TaskId)?.Title ?? "Нет доступа",
                    Status = reportEntity.Status.ToString(),
                    Description = reportEntity.Description,
                });
                return this.PartialView("TimeReportsFilters", reportsModel);
            }
        }

        public async Task<PartialViewResult> FilterWeekOrMonth(string value)
        {
            DateTime DateNow = DateTime.Today;
            DateTime DateWeekLast = DateTime.Today.AddDays(-7);
            DateTime DateMonthLast = DateTime.Today.AddDays(-30);
            if (value == "A week")
            {
                IEnumerable<TimeReportEntity> allTasks = await unitOfWork.TimeReportRepository.GetAll();

                IList<TimeReportModel> tasksModel = new List<TimeReportModel>();
                {
                    foreach (TimeReportEntity report in allTasks)
                    {
                        if (report.StartDate >= DateWeekLast)
                        {
                            tasksModel.Add(new TimeReportModel()
                            {
                                Id = report.TimeReportId,
                                SpentHours = new ReportSpentHours
                                {
                                    Effort = report.Effort,
                                    Overtime = report.Overtime
                                },
                                TimeInterval = new TimeReportInterval
                                {
                                    StartDate = report.StartDate,
                                    EndDate = report.EndDate
                                },
                                ProjectName = report.Projects?.Name ?? "Нет доступа к экземпляру",
                                TaskName = report.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == report.TaskId)?.Title ?? "Нет доступа",
                                Status = report.Status.ToString(),
                                Description = report.Description,
                            });

                        }
                    }
                }
                return this.PartialView("TimeReportsFilters", tasksModel);
            }
            else
                if (value == "A month")
            {
                IEnumerable<TimeReportEntity> allTasks = await unitOfWork.TimeReportRepository.GetAll();

                IList<TimeReportModel> tasksModel = new List<TimeReportModel>();
                {
                    foreach (TimeReportEntity report in allTasks)
                    {
                        if (report.StartDate >= DateMonthLast)
                        {
                            tasksModel.Add(new TimeReportModel()
                            {
                                Id = report.TimeReportId,
                                SpentHours = new ReportSpentHours
                                {
                                    Effort = report.Effort,
                                    Overtime = report.Overtime
                                },
                                TimeInterval = new TimeReportInterval
                                {
                                    StartDate = report.StartDate,
                                    EndDate = report.EndDate
                                },
                                ProjectName = report.Projects?.Name ?? "Нет доступа к экземпляру",
                                TaskName = report.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == report.TaskId)?.Title ?? "Нет доступа",
                                Status = report.Status.ToString(),
                                Description = report.Description,
                            });

                        }
                    }
                }
                return this.PartialView("TimeReportsFilters", tasksModel);
            }
            else
            {
                IEnumerable<TimeReportEntity> allReports = await this.reportsService.GetAll();

                IEnumerable<TimeReportModel> reportsModel = allReports.Select(reportEntity => new TimeReportModel
                {
                    Id = reportEntity.TimeReportId,
                    SpentHours = new ReportSpentHours
                    {
                        Effort = reportEntity.Effort,
                        Overtime = reportEntity.Overtime
                    },
                    TimeInterval = new TimeReportInterval
                    {
                        StartDate = reportEntity.StartDate,
                        EndDate = reportEntity.EndDate
                    },
                    ProjectName = reportEntity.Projects?.Name ?? "Нет доступа к экземпляру",
                    TaskName = reportEntity.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == reportEntity.TaskId)?.Title ?? "Нет доступа",
                    Status = reportEntity.Status.ToString(),
                    Description = reportEntity.Description,
                });
                return this.PartialView("TimeReportsFilters", reportsModel);
            }
        }

        public async Task<PartialViewResult> FilterDate(string value)
        {
            if (dateEnd == "") dateEnd = Convert.ToString(DateTime.Today.Date);
            IEnumerable<TimeReportEntity> allTasks = await unitOfWork.TimeReportRepository.GetAll();
            IList<TimeReportModel> tasksModel = new List<TimeReportModel>();
            foreach (TimeReportEntity report in allTasks)
            {
                if (report.StartDate >= Convert.ToDateTime(value) && report.EndDate <= Convert.ToDateTime(dateEnd))
                {
                    tasksModel.Add(new TimeReportModel()
                    {
                        Id = report.TimeReportId,
                        SpentHours = new ReportSpentHours
                        {
                            Effort = report.Effort,
                            Overtime = report.Overtime
                        },
                        TimeInterval = new TimeReportInterval
                        {
                            StartDate = report.StartDate,
                            EndDate = report.EndDate
                        },
                        ProjectName = report.Projects?.Name ?? "Нет доступа к экземпляру",
                        TaskName = report.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == report.TaskId)?.Title ?? "Нет доступа",
                        Status = report.Status.ToString(),
                        Description = report.Description,
                    });
                }
            }
            dateStart = value;
            return this.PartialView("TimeReportsFilters", tasksModel);
        }

        public async Task<PartialViewResult> FilterDateEnd(string value)
        {
            if (dateStart == "") dateStart = Convert.ToString(DateTime.Today);
            IEnumerable<TimeReportEntity> allTasks = await unitOfWork.TimeReportRepository.GetAll();
            IList<TimeReportModel> tasksModel = new List<TimeReportModel>();
            foreach (TimeReportEntity report in allTasks)
            {
                if (report.StartDate >= Convert.ToDateTime(dateStart) && report.EndDate <= Convert.ToDateTime(value))
                {
                    tasksModel.Add(new TimeReportModel()
                    {
                        Id = report.TimeReportId,
                        SpentHours = new ReportSpentHours
                        {
                            Effort = report.Effort,
                            Overtime = report.Overtime
                        },
                        TimeInterval = new TimeReportInterval
                        {
                            StartDate = report.StartDate,
                            EndDate = report.EndDate
                        },
                        ProjectName = report.Projects?.Name ?? "Нет доступа к экземпляру",
                        TaskName = report.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == report.TaskId)?.Title ?? "Нет доступа",
                        Status = report.Status.ToString(),
                        Description = report.Description,
                    });
                }

            }
            dateEnd = value;
            return this.PartialView("TimeReportsFilters", tasksModel);
        }

        [Authorize(Roles = "1, 2")]
        [HttpPost]
        public async Task<RedirectToRouteResult> Save(ManageTimeReportModel postModel)
        {
            postModel.Projects = await unitOfWork.ProjectRepository.GetAll();
            var accounts = await unitOfWork.AccountRepository.GetAll();
            int accountId = accounts.FirstOrDefault(m => m.Login == User.Identity.Name).AccountId;

            TimeReportEntity newReport = new TimeReportEntity
            {
                TimeReportId = postModel.Id,
                ProjectId = postModel.ProjectId,
                AccountId = accountId,
                Effort = postModel.SpentHours.Effort,
                Overtime = postModel.SpentHours.Overtime,
                StartDate = postModel.TimeInterval.StartDate,
                EndDate = postModel.TimeInterval.EndDate,
                Status = ReportStatus.Open,
                TaskId = postModel.TaskId, //todo: if PostModel.ProjectId != Task.Project.Id => Exception - таски должны соответствовать проекту
                Description = postModel.Description
            };

            var report = await unitOfWork.TimeReportRepository.GetByID(postModel.Id);

            //try
            //{
            if (report == null)
            {
                await this.reportsService.Insert(newReport);
            }
            else
            {
                await reportsService.Update(newReport);
            }
            //}
            //catch (Exception e)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            //}

            //return new HttpStatusCodeResult(HttpStatusCode.OK);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet]
        public async Task<PartialViewResult> CreateTimeReport()
        {
            //TODO : Таски соответствуют проекту
            //IEnumerable<TaskEntity> tasksEntities = unitOfWork.TaskRepository.GetAll();//задачи из базы данных

            IEnumerable<TimeReportEntity> allTimeReport = await unitOfWork.TimeReportRepository.GetAll();

            IEnumerable<ProjectEntity> projectEntities = await unitOfWork.ProjectRepository.GetAll();

            IEnumerable<TaskEntity> tasksEntities = allTasks.Select(taskEntity => new TaskEntity//задачи из словаря
            {
                TaskId = taskEntity.Key,
                Title = taskEntity.Value,
            });

            var createModel = new ManageTimeReportModel
            {
                Tasks = tasksEntities,
                Projects = projectEntities,
                Id = allTimeReport.Last().TimeReportId + 1
            };

            return this.PartialView("EditableTimeReportRow", createModel);
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet]
        public async Task<PartialViewResult> EditTimeReport(int id)
        {
            TimeReportEntity reportEntity = await unitOfWork.TimeReportRepository.GetByID(id);

            var model = new ManageTimeReportModel
            {
                Id = reportEntity.TimeReportId,
                SpentHours = new ReportSpentHours
                {
                    Effort = reportEntity.Effort,
                    Overtime = reportEntity.Overtime
                },
                TimeInterval = new TimeReportInterval
                {
                    StartDate = reportEntity.StartDate,
                    EndDate = reportEntity.EndDate
                },
                ProjectId = reportEntity.Projects?.ProjectId ?? 1000,
                TaskId = reportEntity.TaskId,
                //Tasks = unitOfWork.TaskRepository.GetAll(), //todo: проверить чтобы таски соответствовали проекту!!!!
                Tasks = allTasks.Select(taskEntity => new TaskEntity//задачи из словаря
                {
                    TaskId = taskEntity.Key,
                    Title = taskEntity.Value,
                }),
                Projects = await unitOfWork.ProjectRepository.GetAll(),
                Description = reportEntity.Description
            };

            return this.PartialView("EditableTimeReportRow", model);
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet]
        public async Task<RedirectToRouteResult> DeleteTimeReport(int id)
        {
            await reportsService.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "1, 2")]
        [HttpPost]
        public async Task<RedirectToRouteResult> NotifyTimeReport(int[] reports)
        {
            foreach (int report in reports)
            {
                await reportsService.Notify(report);
            }

            return RedirectToAction("Index");
        }
    }
}