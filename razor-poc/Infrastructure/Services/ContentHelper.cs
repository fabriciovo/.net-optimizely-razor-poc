using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.Core.Internal;
using EPiServer.DataAccess.Internal;
using EPiServer.Editor;
using EPiServer.Globalization;
using EPiServer.Web;
using razor_poc.Models.Pages;

namespace razor_poc.Infrastructure.Services
{
    public class ContentHelper : IContentHelper
    {
        private readonly IContentLoader _contentLoader;
        private readonly IContentVersionRepository _contentVersionRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        public ContentHelper(IContentLoader contentLoader, IContentVersionRepository contentVersionRepository, IContentRepository contentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _contentLoader = contentLoader;
            _contentVersionRepository = contentVersionRepository;
            _contentRepository = contentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public ContextMode GetContextMode()
        {
            var httpCtx = _httpContextAccessor;
            var editModeValue = httpCtx.HttpContext?.Request?.Query[PageEditing.EpiEditMode];
            if (string.IsNullOrEmpty(editModeValue))
            {
                return ContextMode.Default;
            }
            if (bool.TryParse(editModeValue, out bool editMode))
            {
                return editMode ? ContextMode.Edit : ContextMode.Preview;
            }
            return ContextMode.Undefined;
        }

        public IHomePage? GetHomePage()
        {
            return _contentLoader.TryGet<IHomePage>(ContentReference.StartPage, out var homePage) ? homePage : null;
        }

        public SiteSettingsPage? GetSiteSettingPage()
        {
            var homePage = GetHomePage();
            if (homePage == null) return null;
            return _contentLoader.TryGet<SiteSettingsPage>(homePage.SiteSettingsPageLink, out var siteSettings) ? siteSettings : null;
        }

        public T? GetContent<T>(ContentReference contentReference) where T : IContentData
        {
            var loaderOptions = new LoaderOptions().Add(LanguageLoaderOption.Fallback(ContentLanguage.PreferredCulture));
            return _contentLoader.TryGet<T>(contentReference, loaderOptions, out var content) ? content : default;
        }

        public T? GetDraftContent<T>(ContentReference contentReference) where T : IContentData
        {           
            var version = _contentVersionRepository.LoadCommonDraft(contentReference, ContentLanguage.PreferredCulture.Name);

            var loaderOptions = new LoaderOptions().Add(LanguageLoaderOption.FallbackWithMaster(ContentLanguage.PreferredCulture));

            if (_contentRepository.TryGet<T>(version.ContentLink, loaderOptions, out var content))
            {               
                return  content;
            }

            return GetContent<T>(contentReference);
        }

    }
}
