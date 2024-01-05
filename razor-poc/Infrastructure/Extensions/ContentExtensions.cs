using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using razor_poc.Infrastructure.Services;

namespace razor_poc.Infrastructure.Extensions
{
    public static class ContentExtensions
    {
        private static Injected<IContentHelper> ContentHelper;
        public static string GetFriendlyUrl(this ContentReference contentLink)
        {
            if (ContentReference.IsNullOrEmpty(contentLink)) return string.Empty;
            var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
            if (urlResolver == null) return string.Empty;

            var result = urlResolver.GetUrl(
                    contentLink,
                    ContentLanguage.PreferredCulture.Name,
                    new VirtualPathArguments { ContextMode = ContextMode.Default, ForceCanonical = true });

            return result;
        }

        public static string GetFriendlyUrl(this string contentUrl)
        {
            if (string.IsNullOrEmpty(contentUrl)) return string.Empty;
            var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
            if (urlResolver == null) return string.Empty;
            var result = urlResolver.GetUrl(contentUrl);
            return result;
        }
       
        public static IContent GetContent(this Url contentUrl, out string publicUrl)
        {
            if (contentUrl == null) { publicUrl = string.Empty; return null; }
            var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
            if (contentUrl.IsAbsoluteUri)
            {
                publicUrl = contentUrl.Uri.AbsoluteUri;
            }
            else
            {
                publicUrl = urlResolver.GetUrl(contentUrl.PathAndQuery);
            }
            var url = new UrlBuilder(contentUrl);
            if (urlResolver == null) return null;
            var content = urlResolver.Route(url);
            return content;

        }

        public static T? GetContent<T>(this ContentReference contentReference) where T : IContentData
        {
            return ContentHelper.Service.GetContent<T>(contentReference);
        }
    }
}
