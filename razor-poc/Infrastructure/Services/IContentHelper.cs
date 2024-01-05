using EPiServer.Web;
using razor_poc.Models.Pages;

namespace razor_poc.Infrastructure.Services
{
    public interface IContentHelper
    {
        IHomePage? GetHomePage();
        SiteSettingsPage? GetSiteSettingPage();
        T? GetContent<T>(ContentReference contentReference) where T : IContentData;
        T? GetDraftContent<T>(ContentReference contentReference) where T : IContentData;
        ContextMode GetContextMode();
    }
}