using System.Security.Claims;
namespace ClothingShopping.Store
{
    public class ClaimStore
    {      
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Create","Create"),
            new Claim("Update","Update"),
            new Claim("Delete","Delete")
        };
    }
}
