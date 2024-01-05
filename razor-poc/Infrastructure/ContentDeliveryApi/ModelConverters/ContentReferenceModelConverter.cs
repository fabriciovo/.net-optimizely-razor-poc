using EPiServer.ContentApi.Core.Internal;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Globalization;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using razor_poc.Infrastructure.Extensions;
using razor_poc.Models.Media;

namespace razor_poc.Infrastructure.ContentDeliveryApi.ModelConverters
{
    [Serializable]
    [PropertyDefinitionTypePlugIn]
    public class CustomContentReferenceModel : ContentModelReference
    {
        public string ContentTemplate { get; set; }
        public string AltText { get; set; }
        public bool OpenInNewWindow { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CustomContentReferencePropertyModel : ContentReferencePropertyModel
    {
        private PropertyContentReference _propertyContentReference;
        private readonly Injected<IContentLoader> _contentLoaderProxy;
        public CustomContentReferencePropertyModel(PropertyContentReference propertyContentReference, ConverterContext converterContext) : base(propertyContentReference, converterContext)
        {
            _propertyContentReference = propertyContentReference;
            Value = GetValue();
        }

        private ContentModelReference GetValue()
        {
            if (!ContentReference.IsNullOrEmpty(_propertyContentReference.ContentLink))
            {
                var contentLoader = _contentLoaderProxy.Service;
                if (contentLoader == null) return null;
                var lang = ContentLanguage.PreferredCulture;
                if (!contentLoader.TryGet(PropertyDataProperty.ContentLink, lang, out IContent content))
                    return null;
                var mediaContent = content as IMediaContent;

                var model = new CustomContentReferenceModel
                {
                    Id = _propertyContentReference.ID,
                    WorkId = _propertyContentReference.WorkID,
                    GuidValue = _propertyContentReference.GuidValue,
                    ProviderName = _propertyContentReference.ProviderName,
                    Url = _propertyContentReference.ContentLink.GetFriendlyUrl(),
                    ContentTemplate = content.GetOriginalType().Name,
                };


                if (mediaContent != null)
                {
                    model.Title = mediaContent.Title;
                    model.AltText = mediaContent.AltText;
                }

                return model;
            }

            return null;
        }

    }

    public class CustomPropertyReferenceConverter : IPropertyConverter
    {
        public IPropertyModel Convert(PropertyData propertyData, ConverterContext contentMappingContext)
        {
            return new CustomContentReferencePropertyModel(propertyData as PropertyContentReference, contentMappingContext);
        }
    }
}
