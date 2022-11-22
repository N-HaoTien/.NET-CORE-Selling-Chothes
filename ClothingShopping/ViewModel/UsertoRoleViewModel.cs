namespace ClothingShopping.ViewModel
{
    public class UsertoRoleViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string ?UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserClaim { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public bool IsCheck { get; set; }
        public string NormalizedName { get; set; } = string.Empty;
    }
}
