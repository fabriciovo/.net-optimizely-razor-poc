using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using razor_poc.Infrastructure.Services;
using razor_poc.Models.Blocks;
using razor_poc.Models.ViewModels;

namespace razor_poc.Components
{
    public abstract class BaseBlockComponent<T> : BlockComponent<T> where T : BlockData
    {
        protected Injected<IContentHelper> ContentHelperService;
        protected void RegisterComponent(ComponentView component)
        {
            var list = HttpContext.Items["ComponentsOnPage"] as List<ComponentView> ?? new List<ComponentView>();
            list.Add(component);
            HttpContext.Items["ComponentsOnPage"] = list;
        }

        protected IViewComponentResult BuildView(BaseBlockViewModel<T> blockData)
        {
            return View($"/Views/Shared/Components/{blockData.ComponentName}.cshtml", blockData);
        }
    }
}
   
