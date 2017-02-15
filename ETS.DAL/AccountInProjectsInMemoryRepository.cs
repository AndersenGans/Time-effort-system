using System.Collections.Generic;
using System.Linq;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;

namespace ETS.DAL
{
    public class AccountInProjectsInMemoryRepository : IAccountInProjectsRepository
    {
        private static readonly List<AccountInProjectEntity> AccountInProjectStorage = new List<AccountInProjectEntity>
        {
            new AccountInProjectEntity
            {
                Account = new AccountEntity
                {
                    Id = 12,
                    Name = "John",
                    Surname = "Green",
                    Email = "JohnGreen@gmail.com",
                    Login = "JohnGreen12",
                    Password = "JohnGreen12",
                    AccessLevel = 1
                },
                Project = new ProjectEntity
                {
                    Id = 3,
                    Name = "W3D3",
                    Description = "W3D3_description"
                },
                Role = new RoleEntity
                {
                    Id = 1,
                    Name = "Project manager"
                }
            },
            new AccountInProjectEntity
            {
                Account = new AccountEntity
                {
                    Id = 13,
                    Name = "Lila",
                    Surname = "Brown",
                    Email = "JohnGreen@gmail.com",
                    Login = "JohnGreen12",
                    Password = "JohnGreen12",
                    AccessLevel = 1
                },
                Project = new ProjectEntity
                {
                    Id = 5,
                    Name = "LTA",
                    Description = "LTA_description"
                },
                Role = new RoleEntity
                {
                    Id = 2,
                    Name = "Team lead"
                }
            }
        };

        public IEnumerable<AccountInProjectEntity> GetAll()
        {
            return AccountInProjectStorage;
        }

        //Временный поиск по Id Project
        public AccountInProjectEntity GetById(int id)
        {
            return AccountInProjectStorage.FirstOrDefault(t => t.Project.Id == id);
        }

        public void Add(AccountInProjectEntity role)
        {
            throw new System.NotImplementedException();
        }

        public void Update(AccountInProjectEntity roleWithChanges)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
