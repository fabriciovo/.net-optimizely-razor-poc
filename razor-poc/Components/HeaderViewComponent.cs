using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using razor_poc.Infrastructure.Services;
using razor_poc.Models.ViewModels;

namespace razor_poc.Components
{
    public class HeaderViewComponent : BaseComponent
    {
        private readonly IContentHelper _contentHelper;

        public HeaderViewComponent(IContentHelper contentHelper)
        {
            _contentHelper = contentHelper;
        }

        public IViewComponentResult Invoke()
        {
            var siteSettingsPage = _contentHelper.GetSiteSettingPage();
           
            var data = new SiteSettingsViewModel
            {
                CopyrightText = siteSettingsPage.CopyrightText,
                Favicon = new ImageViewModel(siteSettingsPage.Favicon),
                SiteLogo = new ImageViewModel(siteSettingsPage.SiteLogo),
                FooterLinks = siteSettingsPage.FooterLinks?.Select(x => new LinkItemViewModel(x)) ?? Enumerable.Empty<LinkItemViewModel>(),
                FooterLinksTitle = siteSettingsPage.FooterLinksTitle
            };

            return BuildView("Header", "header_1", data);
           
        }
    }
}
