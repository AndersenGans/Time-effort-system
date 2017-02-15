using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;
using ETS.DAL;
using WebSite.Models;
using System.Threading.Tasks;

namespace WebSite.Controllers
{
    [Authorize]
    public class ProjectManagementController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //Зарезервированные задачи на проектах
        private readonly IDictionary<int, string> allTasks = new Dictionary<int, string>()
        {
            { 1, "Development" },
            { 2, "Testing" },
            { 3, "Bug fixing" }
        };

     
        public async Task<PartialViewResult> FilterProjectName(int id)
        {
            if (id != -1)
            { //One project
                IEnumerable<ProjectEntity> allProjects = await unitOfWork.ProjectRepository.GetAll();

                ProjectEntity projectSelect = allProjects.FirstOrDefault(project => project.ProjectId == id);

                IList<ProjectManagementModel> projectsModel = new List<ProjectManagementModel>()
                {
                    new ProjectManagementModel
                    {
                        Id = projectSelect.ProjectId,
                        ProjectName = projectSelect.Name,
                        ProjectManager = projectSelect.Teammates?.FirstOrDefault(acc => acc.RoleId == 3)?.Account?.NameSurname ?? "Нет менеджера",
                        Description = projectSelect.Description
                    }
                };

                return this.PartialView("ProjectFilters", projectsModel);
            }
            else
            { //All projects
                IEnumerable<ProjectEntity> allProjects = await unitOfWork.ProjectRepository.GetAll();

                IEnumerable<ProjectManagementModel> projectsModel = allProjects.Select(projectEntity => new ProjectManagementModel
                {
                    Id = projectEntity.ProjectId,
                    ProjectName = projectEntity.Name,
                    ProjectManager = projectEntity.Teammates?.FirstOrDefault(acc => acc.RoleId == 3)?.Account?.NameSurname ?? "Нет менеджера",
                    Description = projectEntity.Description
                });

                return this.PartialView("ProjectFilters", projectsModel);
            }
        }

        public async Task<PartialViewResult> FilterProjectManager(string name)
        {
            IEnumerable<ProjectEntity> allProjects = await unitOfWork.ProjectRepository.GetAll();

            IList<Teammate> teammates = new List<Teammate>();
            IList<ProjectManagementModel> projectManager = new List<ProjectManagementModel>();
            IEnumerable<AccountEntity> accounts = await unitOfWork.AccountRepository.GetAll();

            int id = accounts.FirstOrDefault(account => account.NameSurname == name).AccountId;//Id сотрудника, по которому был воспроизведен поиск

            foreach (var item_project in allProjects)
            {
                foreach (var item_teammate in item_project.Teammates)
                {
                    teammates.Add(new Teammate
                    {
                        AccountId = item_teammate.AccountId,
                        ProjectId = item_teammate.ProjectId,
                        RoleId = item_teammate.RoleId,
                        Account = item_teammate.Account,
                        Project = item_teammate.Project,
                        Role = item_teammate.Role,
                    });
                }
            }

            foreach (var item in teammates)
            {
                if (item.AccountId == id && item.RoleId == 3) //проверка на выбранного сотрудника и его должность на проекте RoleId(ProjectManager) = 3
                {
                    var projects = await unitOfWork.ProjectRepository.GetAll();
                    var _accounts = await unitOfWork.AccountRepository.GetAll();
                    projectManager.Add(new ProjectManagementModel
                    {
                        Id = item.ProjectId,
                        ProjectName = projects.FirstOrDefault(x => x.ProjectId == item.ProjectId).Name,
                        ProjectManager = _accounts.FirstOrDefault(x => x.AccountId == item.AccountId).NameSurname,
                        Description = projects.FirstOrDefault(x => x.ProjectId == item.ProjectId).Description,
                    });
                }
            }

            return this.PartialView("ProjectFilters", projectManager);
        }
        // GET: ProjectManagement
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<ProjectEntity> allProjects = await unitOfWork.ProjectRepository.GetAll();

            IEnumerable<ProjectManagementModel> projectsModel = allProjects.Select(projectEntity => new ProjectManagementModel
            {
                Id = projectEntity.ProjectId,
                ProjectName = projectEntity.Name,
                ProjectManager = projectEntity.Teammates?.FirstOrDefault(acc => acc.RoleId == 3)?.Account?.NameSurname ?? "Нет менеджера",
                Description = projectEntity.Description
            });

            return View(projectsModel);
        }

        //Формирование страницы EditProject
        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<ActionResult> EditProject(int id)
        {
            var projects = await unitOfWork.ProjectRepository.GetByID(id);
            if (projects != null)
            {
                ProjectEntity projectEntity = await unitOfWork.ProjectRepository.GetByID(id);
                IEnumerable<AccountEntity> allAccountEntity = await unitOfWork.AccountRepository.GetAll();
                IEnumerable<TaskEntity> allTaskEntity = await unitOfWork.TaskRepository.GetAll();
                IEnumerable<RoleEntity> roleByProjectEntity = await unitOfWork.RoleRepository.GetAll();
                IEnumerable<Teammate> teammatesInProject = projectEntity.Teammates;

                IList<TaskEntity> tasksInProject = new List<TaskEntity>();
                foreach (var task in allTaskEntity)
                {
                    if (task.ProjectId == projectEntity.ProjectId)
                    {
                        tasksInProject.Add(
                            new TaskEntity
                            {
                                TaskId = task.TaskId,
                                Title = task.Title,
                                ProjectId = task.ProjectId,
                                Description = task.Description
                            });
                    }
                }

                var model = new ManageProjectManagementModel
                {
                    ProjectId = projectEntity.ProjectId,
                    ProjectName = projectEntity.Name,
                    Description = projectEntity.Description,
                    Task = allTasks.Select(taskEntity => new TaskEntity
                    {
                        TaskId = taskEntity.Key,
                        Title = taskEntity.Value,
                    }),
                    TasksInProject = tasksInProject,
                    TeamMember = teammatesInProject.Select(accountEntity => new AccountEntity
                    {
                        AccountId = accountEntity.AccountId,
                        Name = accountEntity.Account.Name,
                        Surname = accountEntity.Account.Surname
                    }),
                    Role = teammatesInProject.Select(roleEntity => new RoleEntity
                    {
                        RoleId = roleEntity.RoleId,
                        Name = roleEntity.Role.Name,
                    }),
                    TeammatesInProject = teammatesInProject
                };

                return View("EditProject", model);
            }
            else
            {
                //если это новый ид (добавление нового проекта)
                ProjectEntity projectEntity = await unitOfWork.ProjectRepository.GetByID(id);
                IEnumerable<AccountEntity> allAccountEntity = await unitOfWork.AccountRepository.GetAll();
                IEnumerable<TaskEntity> allTaskEntity = await unitOfWork.TaskRepository.GetAll();
                IEnumerable<RoleEntity> roleByProjectEntity = await unitOfWork.RoleRepository.GetAll();

                var newProjectModel = new ManageProjectManagementModel
                {
                    ProjectId = id,
                    ProjectName = "",
                    Description = "",
                    Task = allTasks.Select(taskEntity => new TaskEntity
                    {
                        TaskId = taskEntity.Key,
                        Title = taskEntity.Value,
                    }),
                    TeamMember = allAccountEntity.Select(accountEntity => new AccountEntity
                    {
                        AccountId = accountEntity.AccountId,
                        Name = accountEntity.Name,
                        Surname = accountEntity.Surname
                    }),
                    Role = roleByProjectEntity.Select(roleEntity => new RoleEntity
                    {
                        RoleId = roleEntity.RoleId,
                        Name = roleEntity.Name,
                    })
                };

                return View("EditProject", newProjectModel);
            }
        }

        //Изменение данных о проекте
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<RedirectToRouteResult> UpdateProject(ManageProjectManagementModel model, List<string> idTasks, List<string> idTeamMember, List<string> idRoleInProject)
        {
            var projects = await unitOfWork.ProjectRepository.GetByID(model.ProjectId);
            if (projects == null)
            {
                IEnumerable<AccountEntity> allAccountEntity = await unitOfWork.AccountRepository.GetAll();
                IEnumerable<TaskEntity> allTaskEntity = await unitOfWork.TaskRepository.GetAll();
                IEnumerable<RoleEntity> roleByProjectEntity = await unitOfWork.RoleRepository.GetAll();

                ProjectEntity projectEntity = new ProjectEntity();
                projectEntity.ProjectId = model.ProjectId;
                projectEntity.Name = model.ProjectName;
                projectEntity.Description = model.Description;

                IList<TaskEntity> tasksInProject = new List<TaskEntity>();
                IList<Teammate> teammatesInProject = new List<Teammate>();

                foreach (var task in idTasks)
                {
                    tasksInProject.Add(new TaskEntity
                    {
                        TaskId = allTaskEntity.Count() + 1,
                        Title = allTasks.FirstOrDefault(m => m.Key == int.Parse(task)).Value,
                        ProjectId = model.ProjectId
                    });
                }

                int i = 0;
                foreach (var teammate in idTeamMember)
                {
                    teammatesInProject.Add(new Teammate
                    {
                        AccountId = int.Parse(teammate),
                        ProjectId = model.ProjectId,
                        RoleId = int.Parse(idRoleInProject[i++])
                    });
                }

                projectEntity.Tasks = tasksInProject;
                projectEntity.Teammates = teammatesInProject;

                unitOfWork.ProjectRepository.Insert(projectEntity);
                unitOfWork.Save();
            }
            else
            {
                IEnumerable<AccountEntity> allAccountEntity = await unitOfWork.AccountRepository.GetAll();
                IEnumerable<TaskEntity> allTaskEntity = await unitOfWork.TaskRepository.GetAll();
                IEnumerable<RoleEntity> roleByProjectEntity = await unitOfWork.RoleRepository.GetAll();

                ProjectEntity projectEntity = await unitOfWork.ProjectRepository.GetByID(model.ProjectId);

                projectEntity.ProjectId = model.ProjectId;
                projectEntity.Name = model.ProjectName;
                projectEntity.Description = model.Description;

                if (idTasks != null)
                {
                    for (int j = 0; j < projectEntity.Tasks.Count; j++)
                    {
                        if (idTasks.FirstOrDefault(m => int.Parse(m) == projectEntity.Tasks.ElementAt(j).TaskId) == null)
                        {
                            unitOfWork.TaskRepository.Delete(projectEntity.Tasks.ElementAt(j));
                            unitOfWork.Save();
                        }
                    }

                    foreach (var task in idTasks)
                    {
                        if (projectEntity.Tasks.FirstOrDefault(m => m.TaskId == int.Parse(task)) == null)
                        {
                            projectEntity.Tasks.Add(new TaskEntity
                            {
                                TaskId = allTaskEntity.Last().TaskId + 1,
                                Title = allTasks.FirstOrDefault(m => m.Key == int.Parse(task)).Value,
                                ProjectId = model.ProjectId,
                                Description = ""
                            });
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < projectEntity.Tasks.Count; j++)
                    {
                        unitOfWork.TaskRepository.Delete(projectEntity.Tasks.ElementAt(j));
                        unitOfWork.Save();
                    }
                }

                if (idTeamMember != null)
                {
                    for (int j = 0; j < projectEntity.Teammates.Count; j++)
                    {
                        if (idTeamMember.FirstOrDefault(m => int.Parse(m) == projectEntity.Teammates.ElementAt(j).AccountId) == null)
                        {
                            projectEntity.Teammates.Remove(projectEntity.Teammates.ElementAt(j));
                        }
                    }
                    int i = 0;
                    foreach (var teammate in idTeamMember)
                    {
                        var proj = await unitOfWork.ProjectRepository.GetAll();
                        if (proj.FirstOrDefault(x => x.ProjectId == model.ProjectId).Teammates.FirstOrDefault(m => m.AccountId == int.Parse(teammate)) == null)
                        {
                            projectEntity.Teammates.Add(new Teammate
                            {
                                AccountId = int.Parse(teammate),
                                ProjectId = model.ProjectId,
                                RoleId = int.Parse(idRoleInProject[i]),
                                Account = await unitOfWork.AccountRepository.GetByID(int.Parse(teammate)),
                                Project = await unitOfWork.ProjectRepository.GetByID(model.ProjectId),
                                Role = await unitOfWork.RoleRepository.GetByID(int.Parse(idRoleInProject[i++]))
                            });
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < projectEntity.Teammates.Count; j++)
                    {
                        projectEntity.Teammates.Remove(projectEntity.Teammates.ElementAt(j));
                    }
                }

                unitOfWork.ProjectRepository.Update(projectEntity);
                unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }

        //Удаление проекта
        [Authorize(Roles = "1")]
        [HttpGet]
        public RedirectToRouteResult DeleteProject(int id)
        {
            var projects = unitOfWork.ProjectRepository.GetByID(id);
            if (projects != null)
            {
                unitOfWork.ProjectRepository.Delete(id);
                unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }
    }
}