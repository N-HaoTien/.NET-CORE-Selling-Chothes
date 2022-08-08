using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ClothingShopping
{
    public class Helper
    {
        public static string RenderRazorView(Controller controller, string ViewName, object model = null)
        {
            controller.ViewData.Model = model;
            using(var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult result = viewEngine.FindView(controller.ControllerContext, ViewName, false);
                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    result.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions()
                    );
                result.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
