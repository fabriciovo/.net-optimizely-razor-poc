using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using razor_poc.Infrastructure.Services;
using razor_poc.Models.Pages;

namespace razor_poc.Controllers
{
    [TemplateDescriptor(Inherited = true)]
    public class DefaultPageController : BasePageController<BasePage>
    {      
       
        public IActionResult Index()
        {         
            var currentPage = PageContext.Page as BasePage;
            var contextMode = ContentHelperService.Service.GetContextMode();
            if (contextMode == EPiServer.Web.ContextMode.Edit || contextMode == EPiServer.Web.ContextMode.Preview)
            {
                currentPage = ContentHelperService.Service.GetDraftContent<BasePage>(currentPage.ContentLink);
            }

            return View($"~/Views/{currentPage.GetOriginalType().Name}/Index.cshtml", currentPage);         
        }
    }    

}
