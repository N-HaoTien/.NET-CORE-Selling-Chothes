using ClothingShopping.Models;
using ClothingShoppingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShoppingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<UserTest> listUser = new List<UserTest>()
        {
             new UserTest{Id = 1,Name = "A",Password="1",TaiKhoan = "1",roles = "Admin" },
             new UserTest{Id = 2,Name = "B",Password="2",TaiKhoan = "2",roles = "Quản Lý" },
             new UserTest{Id = 3,Name = "C",Password="3",TaiKhoan = "3",roles = "Nhân Viên" },
        };
        public static List<UserClaimTest> listClaim = new List<UserClaimTest>()
        {
            new UserClaimTest{Id = 1,UserId=1,Value = "Thêm"},
            new UserClaimTest{Id = 1,UserId=1,Value = "Xóa"},
            new UserClaimTest{Id = 1,UserId=1,Value = "Sửa"}
        };
        [HttpGet]

        public IActionResult GetListUser()
        {
            return Ok(getAll());
        }
        [NonAction]
        public List<UserTest> getAll()
        {
            return listUser;
        }
        [HttpPost]
        public IActionResult CheckBoolean(string user, string pass)
        {
            foreach (var item in listUser)
            {
                if (item.TaiKhoan == user && item.Password == pass)
                {
                    return Ok(true);
                }
            }
            return Ok(false);

        }
        [HttpGet]
        public IActionResult CheckLogin(string user, string pass)
        {
            foreach (var item in listUser)
            {
                if (item.TaiKhoan == user && item.Password == pass)
                {
                    return Ok(item);
                }
            }
            return Ok(false);

        }
        [HttpGet]
        public IActionResult Details(string name)
        {
            var userDetails = listUser.Where(p => p.Name == name).FirstOrDefault();
            if(userDetails == null)
            {
                return Ok(false);
            }
            return Ok(userDetails);

        }
    }
}
