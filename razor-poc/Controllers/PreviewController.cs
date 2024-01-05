using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web.Mvc;
using EPiServer.Framework.Web;
using EPiServer.Globalization;
using EPiServer.Web.Mvc;
using EPiServer.Web;
using Microsoft.AspNetCore.Mvc;
using razor_poc.Infrastructure.Extensions;
using razor_poc.Models.Pages;
using EPiServer.Editor;

namespace razor_poc.Controllers
{
    [TemplateDescriptor(Inherited = true, TemplateTypeCategory = TemplateTypeCategories.MvcController, Tags = new[] { RenderingTags.Preview, RenderingTags.Edit }, AvailableWithoutTag = false)]
    [VisitorGroupImpersonation]
    [RequireClientResources]
    public class PreviewController : ActionControllerBase, IRenderTemplate<BasePage>
    {
        private IContextModeResolver _contextModeResolver;

        public PreviewController(IContextModeResolver contextModeResolver)
        {
            _contextModeResolver = contextModeResolver;
        }

        public IActionResult Index(IContent CurrentContent)
        {
           // var isEditMode = _contextModeResolver.CurrentMode.EditOrPreview();
            var url = CurrentContent.ContentLink.GetFriendlyUrl(); ;
          
            return Redirect(url + $"?{PageEditing.EpiEditMode}=true");
           
        }
    }
}
