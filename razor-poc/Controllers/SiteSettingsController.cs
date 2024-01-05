using Microsoft.AspNetCore.Mvc;
using razor_poc.Infrastructure.Services;
using razor_poc.Models.ViewModels;

namespace razor_poc.Controllers
{
    [ApiController]
    [Route("api/site")]
    public class SiteSettingsController : Controller
    {
        private readonly IContentHelper _contentHelper;

       
        public SiteSettingsController(IContentHelper contentHelper)
        {
            _contentHelper = contentHelper;
        }

        [HttpGet]
        [Route("settings", Name = "Get Site Settings")]
        public IActionResult GetSiteSettings()
        {
            var siteSettingsPage = _contentHelper.GetSiteSettingPage();
            if(siteSettingsPage == null)
                return NotFound();

            var data = new SiteSettingsViewModel {
                CopyrightText  = siteSettingsPage.CopyrightText,
                Favicon = new ImageViewModel(siteSettingsPage.Favicon),
                SiteLogo = new ImageViewModel(siteSettingsPage.SiteLogo),
                FooterLinks = siteSettingsPage.FooterLinks?.Select(x=> new LinkItemViewModel(x))??Enumerable.Empty<LinkItemViewModel>(),
                FooterLinksTitle = siteSettingsPage.FooterLinksTitle
            };
            return Json(data);
        }
    }
}
