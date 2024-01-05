using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.SpecializedProperties;
using razor_poc.Infrastructure.ContentDeliveryApi;
using razor_poc.Infrastructure.Extensions;
using razor_poc.Models.Media;
using razor_poc.Models.Pages;

namespace razor_poc.Infrastructure.ContentDeliveryApi.ModelConverters
{
    [Serializable]
    public class CustomLinkItemNode : LinkItemNode
    {

        public CustomLinkItemNode(string href, string title, string target, string text) : base(href, title, target, text)
        {
            if (!string.IsNullOrEmpty(href))
            {

                var content = new Url(href).GetContent(out var publicUrl);
                var mediaContent = content as IMediaContent;
                var pageContent = content as BasePage;
                UrlType = content == null ? UrlType.External : UrlType.Internal;
                ContentType = content == null ? ContentType.Unknown : mediaContent != null ? ContentType.Media : ContentType.Page;


                Title = title ?? mediaContent?.Title ?? pageContent?.Title;
                if (text != "-")
                {
                    Text = text;
                    if (text.Equals(content?.Name, StringComparison.InvariantCultureIgnoreCase))
                        Text = mediaContent?.Title ?? pageContent?.Title;
                }
                else Text = "";

                AltText = mediaContent?.AltText;
                Href = href.GetFriendlyUrl();
            }
        }

        public string? Description { get; set; }
        public string? AltText { get; set; }
        public UrlType UrlType { get; set; }
        public ContentType ContentType { get; set; }
    }


    public class CustomLinkCollectionPropertyModel : LinkCollectionPropertyModel
    {
        private readonly PropertyLinkCollection _propertyLinkCollection;

        public CustomLinkCollectionPropertyModel(PropertyLinkCollection propertyLinkCollection, ConverterContext converterContext) : base(propertyLinkCollection, converterContext)
        {
            _propertyLinkCollection = propertyLinkCollection;
            Value = GetValue();
        }


        private IEnumerable<LinkItemNode> GetValue()
        {
            if (_propertyLinkCollection.Links != null && _propertyLinkCollection.Links.Any())
            {
                return _propertyLinkCollection.Links.Select((x) => new CustomLinkItemNode(x.Href, x.Title, x.Target, x.Text)).ToList();
            }

            return new List<LinkItemNode>();
        }

    }

    public class CustomLinkItemNodeModel : LinkItemPropertyModel
    {
        private readonly PropertyLinkItem _propertyLinkItem;

        public CustomLinkItemNodeModel(PropertyLinkItem propertyLinkItem, ConverterContext converterContext) : base(propertyLinkItem, converterContext)
        {
            _propertyLinkItem = propertyLinkItem;
            Value = GetValue();
        }


        private LinkItemNode GetValue()
        {
            return new CustomLinkItemNode(_propertyLinkItem.Link.Href, _propertyLinkItem.Link.Title, _propertyLinkItem.Link.Target, _propertyLinkItem.Link.Text);
        }

    }

    public class CustomLinkCollectionConverter : IPropertyConverter
    {
        public IPropertyModel Convert(PropertyData propertyData, ConverterContext contentMappingContext)
        {
            return new CustomLinkCollectionPropertyModel(propertyData as PropertyLinkCollection, contentMappingContext);
        }
    }

    public class CustomLinkItemConverter : IPropertyConverter
    {
        public IPropertyModel Convert(PropertyData propertyData, ConverterContext contentMappingContext)
        {
            return new CustomLinkItemNodeModel(propertyData as PropertyLinkItem, contentMappingContext);
        }
    }
}
