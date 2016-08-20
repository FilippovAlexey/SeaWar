namespace SeaWarServer.DTO
{
    public class SetRoleDTO
    {
        public string PlayerId { get; set; }
        public Models.User.RoleEnum Role { get; set; }
    }
}