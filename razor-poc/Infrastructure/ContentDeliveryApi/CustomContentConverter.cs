using EPiServer.Cms.Shell;
using EPiServer.ContentApi.Core.Internal;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Editor;
using EPiServer.Globalization;
using EPiServer.Web;
using razor_poc.Infrastructure.Services;

namespace razor_poc.Infrastructure.ContentDeliveryApi
{/// <summary>
/// Simplifying Content API model
/// </summary>
    public class CustomContentConverterProvider : IContentConverterProvider
    {
        public int SortOrder => 150;
        private readonly IContentConverter _defaultContentConverter;
        
        public CustomContentConverterProvider(IContentConverter defaultContentConverter)
        {
            _defaultContentConverter = defaultContentConverter;           
        }
        public IContentConverter Resolve(IContent content)
        {
            return _defaultContentConverter;
        }
    }
    public class CustomContentConverter : DefaultContentConverter, IContentConverter
    {
        private readonly IContentHelper _contentHelper;


        public CustomContentConverter(IContentTypeRepository contentTypeRepository, ReflectionService reflectionService,
            IContentModelReferenceConverter contentModelService, IContentVersionRepository contentVersionRepository,
            ContentLoaderService contentLoaderService, UrlResolverService urlResolverService, IPropertyConverterResolver propertyConverterResolver,
            ContentTypeResolver contentTypeResolver, IContentHelper contentHelper) : base(contentTypeRepository, reflectionService, contentModelService, contentVersionRepository, contentLoaderService, urlResolverService,
                propertyConverterResolver, contentTypeResolver)
        {
            _contentHelper = contentHelper;           
        }

        public override ContentApiModel Convert(IContent content, ConverterContext converterContext)
        {
            var contextMode = _contentHelper.GetContextMode();
            if(contextMode is ContextMode.Edit)
            {
                var draftcontent = _contentHelper.GetDraftContent<IContent>(content.ContentLink);
                if(draftcontent != null)
                    content = draftcontent;
            }
            if (content is BlockData)
            {
                var language = content.LanguageBranch();
                if (language != ContentLanguage.PreferredCulture.Name)
                {
                    if (contextMode is ContextMode.Edit)
                    {
                        var draftcontent = _contentHelper.GetDraftContent<IContent>(content.ContentLink);
                        if (draftcontent != null)
                            content = draftcontent;
                    }
                    else
                    {
                        var newContent = _contentHelper.GetContent<IContent>(content.ContentLink);
                        if (newContent != null)
                            content = newContent;
                    }
                }
            }

            var contentApiModel = base.Convert(content, converterContext);          

            contentApiModel.Properties.Add("ContentTemplate", content.GetOriginalType().Name);

            return contentApiModel;
        }
      
    }
}
