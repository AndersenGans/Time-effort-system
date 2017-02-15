using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETS.Contracts.DataContracts;
using ETS.DAL;
using WebSite.Models;
using System.Threading.Tasks;

namespace WebSite.Controllers
{
    [Authorize]
    public class TimeEffortController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private IList<TimeEffortModel> timeEfforts = new List<TimeEffortModel>();

        public async Task<PartialViewResult> FilterProjectName(int id)
        {
            if (id != -1)
            { //One project
                IEnumerable<TimeReportEntity> timeReports = await unitOfWork.TimeReportRepository.GetAll();


                TimeReportEntity projectSelect = timeReports.FirstOrDefault(project => project.ProjectId == id);

                IList<TimeEffortModel> projectsModel = new List<TimeEffortModel>()
                {
                    new TimeEffortModel
                    {
                        ProjectId = projectSelect.ProjectId,
                        ProjectName = projectSelect.Projects.Name,
                        Effort = projectSelect.Effort,
                        Overtime = projectSelect.Overtime,
                        Total = projectSelect.Effort + projectSelect.Overtime
                    }
                };

                return this.PartialView("ProjectFilters", projectsModel);
            }
            else
            { //All projects
                IEnumerable<TimeReportEntity> timeReports = await unitOfWork.TimeReportRepository.GetAll();

                foreach (var report in timeReports)
                {
                    var projectEffort = timeEfforts.FirstOrDefault(x => x.ProjectId == report.Projects?.ProjectId);

                    if (projectEffort == null)
                    {
                        //project is not exist in timeEffort list
                        timeEfforts.Add(new TimeEffortModel()
                        {
                            ProjectId = report.ProjectId,
                            ProjectName = report.Projects?.Name ?? "Нет доступа",
                            Effort = report.Effort,
                            Overtime = report.Overtime,
                            Total = report.Effort + report.Overtime
                        });
                    }
                    else
                    {
                        //project exist in timeEffort list
                        projectEffort.Effort += report.Effort;
                        projectEffort.Overtime += report.Overtime;
                        projectEffort.Total = projectEffort.Effort + projectEffort.Overtime;
                    }
                }

                return this.PartialView("ProjectFilters", timeEfforts);
            }
        }


        public async Task<ActionResult> Index()
        {
            IEnumerable<TimeReportEntity> timeReports = await unitOfWork.TimeReportRepository.GetAll();

            foreach (var report in timeReports)
            {
                var projectEffort = timeEfforts.FirstOrDefault(x => x.ProjectId == report.Projects?.ProjectId);

                if (projectEffort == null)
                {
                    //project is not exist in timeEffort list
                    timeEfforts.Add(new TimeEffortModel()
                    {
                        ProjectId = report.ProjectId,
                        ProjectName = report.Projects?.Name??"Нет доступа",
                        Effort = report.Effort,
                        Overtime = report.Overtime,
                        Total = report.Effort + report.Overtime
                    });
                }
                else
                {
                    //project exist in timeEffort list
                    projectEffort.Effort += report.Effort;
                    projectEffort.Overtime += report.Overtime;
                    projectEffort.Total = projectEffort.Effort + projectEffort.Overtime;
                }
            }

            return View(timeEfforts);
        }
    }
}