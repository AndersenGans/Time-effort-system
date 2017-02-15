using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ETS.Contracts.DataContracts;
using ETS.DAL;
using WebSite.Models;
using System.Threading.Tasks;

namespace WebSite.Controllers
{
    [Authorize]
    public class ApprovalController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Approval
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IList<ApprovalModel> model = new List<ApprovalModel>();
            var teamMembers = await unitOfWork.AccountRepository.GetAll();
            var projects = await unitOfWork.ProjectRepository.GetAll();
            var tasks = await unitOfWork.TaskRepository.GetAll();
            foreach (var report in await unitOfWork.TimeReportRepository.GetAll())
            {
                if (report.Status != ReportStatus.Open)
                {
                    model.Add(new ApprovalModel
                    {
                        Id = report.TimeReportId,
                        TeamMember = teamMembers.FirstOrDefault(x => x.AccountId == report.AccountId).NameSurname,
                        Project = projects.FirstOrDefault(x => x.ProjectId == report.ProjectId).Name,
                        Task = tasks.FirstOrDefault(x => x.ProjectId == report.TaskId).Title,
                        Effort = report.Effort,
                        Overtime = report.Overtime,
                        Description = report.Description,
                        StartDate = report.StartDate,
                        EndDate = report.EndDate,
                        Status = report.Status
                    });
                }
        }

            return View("Index", model);
        }

        [Authorize(Roles = "1, 2")]
        public async Task<RedirectToRouteResult> Accept(int id)
        {
            TimeReportEntity report = await unitOfWork.TimeReportRepository.GetByID(id);
            report.Status = ReportStatus.Accepted;

            unitOfWork.TimeReportRepository.Update(report);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "1, 2")]
        public async Task<RedirectToRouteResult> Decline(int id)
        {

            TimeReportEntity report = await unitOfWork.TimeReportRepository.GetByID(id);
            report.Status = ReportStatus.Declined;

            unitOfWork.TimeReportRepository.Update(report);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public async Task<PartialViewResult> FilterProjectName(int id)
        {
            if (id != -1)
            { //One project
                IEnumerable<TimeReportEntity> allTasks = await unitOfWork.TimeReportRepository.GetAll();

                TimeReportEntity taskSelect = allTasks.FirstOrDefault(task => task.TaskId == id);

                IList<ApprovalModel> reportsModel = new List<ApprovalModel>()
                {
                new ApprovalModel
                {
                    Id = taskSelect.TimeReportId,
                    Effort = taskSelect.Effort,
                    Overtime = taskSelect.Overtime,
                    StartDate = taskSelect.StartDate,
                    EndDate = taskSelect.EndDate,
                    Project = taskSelect.Projects?.Name ?? "Нет доступа к экземпляру",
                    Task = taskSelect.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == taskSelect.TaskId)?.Title ?? "Нет доступа",
                    Status = taskSelect.Status,
                    Description = taskSelect.Description,
                }
            };

                return this.PartialView("ApprovalFilters", reportsModel);
            }
            else
            { //All projects
                IEnumerable<TimeReportEntity> allTasks = await unitOfWork.TimeReportRepository.GetAll();

                IEnumerable<ApprovalModel> reportsModel = allTasks.Select(reportEntity => new ApprovalModel
                {
                    Id = reportEntity.TimeReportId,
                        Effort = reportEntity.Effort,
                        Overtime = reportEntity.Overtime,
                        StartDate = reportEntity.StartDate,
                        EndDate = reportEntity.EndDate,
                        Project = reportEntity.Projects?.Name ?? "Нет доступа к экземпляру",
                        Task = reportEntity.Projects?.Tasks?.FirstOrDefault(m => m.TaskId == reportEntity.TaskId)?.Title ?? "Нет доступа",
                        Status = reportEntity.Status,
                        Description = reportEntity.Description,
                });
                return this.PartialView("ApprovalFilters", reportsModel);
            }
        }

    }
}