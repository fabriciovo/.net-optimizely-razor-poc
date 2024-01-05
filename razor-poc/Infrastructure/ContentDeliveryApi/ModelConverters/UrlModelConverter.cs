using EPiServer;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.SpecializedProperties;
using razor_poc.Infrastructure.Extensions;
using razor_poc.Models.Media;
using razor_poc.Models.Pages;
using ContentType = razor_poc.Infrastructure.ContentDeliveryApi.ContentType;

namespace razor_poc.Infrastructure.ContentDeliveryApi.ModelConverters
{
    public class CustomUrl
    {
        public string? Url { get; set; }
        public string? AltText { get; set; }
        public string? Title { get; set; }
        public UrlType UrlType { get; set; }
        public ContentType ContentType { get; set; }
        public bool OpenInNewWindow { get; set; }
    }

    public class CustomUrlModel : PropertyModel<CustomUrl, PropertyUrl>
    {
        public CustomUrlModel(PropertyUrl propertyUrl) : base(propertyUrl)
        {
            var content = propertyUrl.Url.GetContent(out var publicUrl);
            var mediaContent = content as IMediaContent;
            var listingContent = content as BasePage;
            Value = new CustomUrl()
            {
                Url = publicUrl,
                AltText = mediaContent?.AltText ?? listingContent?.Title ?? content.Name,
                Title = listingContent?.Title ?? mediaContent?.Title ?? content?.Name,
                UrlType = content == null ? UrlType.External : UrlType.Internal,
                ContentType = content == null ? ContentType.Unknown : mediaContent != null ? ContentType.Media : ContentType.Page,
                OpenInNewWindow = listingContent?.OpenInNewWindow ?? false
            };
        }
    }

    public class UrlModelConverter : IPropertyConverter
    {
        public IPropertyModel Convert(PropertyData propertyData, ConverterContext contentMappingContext)
        {
            return new CustomUrlModel(propertyData as PropertyUrl);
        }
    }

}
