using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using razor_poc.Infrastructure.Services;
using razor_poc.Models.Pages;
using razor_poc.Models.ViewModels;

namespace razor_poc.Controllers
{
    public class BasePageController<T> : PageController<T> where T : BasePage
    {
        protected Injected<IContentHelper> ContentHelperService;        
    }


}
