using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Mvc;
using razor_poc.Infrastructure.Services;
using razor_poc.Models.ViewModels;

namespace razor_poc.Components
{
    public abstract class BaseComponent : ViewComponent
    {
        protected Injected<IContentHelper> ContentHelperService;
        protected void RegisterComponent(ComponentView component)
        {
            var list = HttpContext.Items["ComponentsOnPage"] as List<ComponentView> ?? new List<ComponentView>();
            list.Add(component);
            HttpContext.Items["ComponentsOnPage"] = list;
        }

        protected IViewComponentResult BuildView(string componentName, string id, object componentData)
        {
            return View($"/Views/Shared/Components/{componentName}.cshtml", componentData);
        }
    }
}
