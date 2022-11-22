namespace ClothingShopping.ViewModel
{
    public class UserClaimViewModel
    {
        public UserClaimViewModel()
        {
            Claims = new List<UserClaim>();
        }
        public string UserId { get; set; }

        public List<UserClaim> Claims { get; set; }
    }
}
public class UserClaim
{
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public bool IsCheck { get; set; }
}
