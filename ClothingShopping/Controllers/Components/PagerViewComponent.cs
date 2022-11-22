using ClothingShopping.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace ClothingShopping.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return await Task.FromResult((IViewComponentResult)View("Default",result));
        }
    }
}
