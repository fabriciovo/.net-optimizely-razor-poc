using EPiServer.ContentApi.Core.Internal;
using EPiServer.Editor;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace razor_poc.Infrastructure.ContentDeliveryApi
{
    public class CustomContentLoaderService : ContentLoaderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomContentLoaderService(IContentLoader contentLoader, IPermanentLinkMapper permanentLinkMapper, IContentProviderManager providerManager, IPublishedStateAssessor publishedStateAssessor, IHttpContextAccessor httpContextAccessor) 
            : base(contentLoader, permanentLinkMapper, providerManager, publishedStateAssessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override bool ShouldContentBeExposed(IContent content)
        {
            //In EditMode, unpublished or expired content is still returned
            if (GetContextMode() == ContextMode.Edit)
            {
                return true;
            }
            return base.ShouldContentBeExposed(content);
        }
        private ContextMode GetContextMode()
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
    }
}
