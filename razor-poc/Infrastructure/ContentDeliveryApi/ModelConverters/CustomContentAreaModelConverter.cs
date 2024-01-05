using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ContentApi.Core.Serialization.Models.Internal;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System.Globalization;

namespace razor_poc.Infrastructure.ContentDeliveryApi.ModelConverters
{


    public class CustomContentAreaPropertyModel : ContentAreaPropertyModel
    {
        public CustomContentAreaPropertyModel(PropertyContentArea propertyContentArea, ConverterContext converterContext) : base(propertyContentArea, converterContext)
        {
            ExpandedValue = ExtractExpandedValue(ContentLanguage.PreferredCulture);
        }
    }

}
