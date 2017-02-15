namespace ETS.Contracts.DataContracts
{
    public class AccountInProjectEntity
    {
        public AccountEntity Account { get; set; }

        public ProjectEntity Project { get; set; }

        public RoleEntity Role { get; set; }
    }
}
