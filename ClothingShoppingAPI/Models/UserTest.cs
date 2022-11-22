namespace ClothingShoppingAPI.Models
{
    public class UserTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaiKhoan { get; set; }
        public string Password { get; set; }

        public string roles { get; set; }
        
    }

    public class UserClaimTest
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }
    }
}