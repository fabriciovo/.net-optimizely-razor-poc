using EPiServer.ContentApi.Core.Serialization;
using EPiServer.SpecializedProperties;
using razor_poc.Infrastructure.ContentDeliveryApi.ModelConverters;

namespace razor_poc.Infrastructure.ContentDeliveryApi;

public class CustomPropertyConverterProvider : IPropertyConverterProvider
{
    public int SortOrder => 200;

    public IPropertyConverter Resolve(PropertyData propertyData)
    {
        if (propertyData is PropertyContentReference)
        {
            return new CustomPropertyReferenceConverter();
        }
        if (propertyData is PropertyLinkItem)
        {
            return new CustomLinkItemConverter();
        }
        else
        if (propertyData is PropertyLinkCollection)
        {

            return new CustomLinkCollectionConverter();
        }
       
        else
        if (propertyData is PropertyUrl)
        {
            return new UrlModelConverter();
        }

        return null;
    }
}
