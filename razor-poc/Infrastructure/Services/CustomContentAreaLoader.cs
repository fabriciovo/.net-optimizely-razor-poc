using EPiServer.Construction;
using EPiServer.Editor;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using System.Globalization;

namespace razor_poc.Infrastructure.Services
{
    public class CustomContentAreaLoader : IContentAreaLoader
    {
       
        private readonly IContentHelper _contentHelper;
        private readonly IContextModeResolver _contextModeResolver;
    

        public CustomContentAreaLoader(
          IContentHelper contentHelper, IContextModeResolver contextModeResolver)
        {
            _contentHelper = contentHelper;
            _contextModeResolver = contextModeResolver;          
        }

        public IContentData LoadContent(ContentAreaItem contentAreaItem) => this.Load(contentAreaItem, this._contextModeResolver.CurrentMode);

        public IContent Get(ContentAreaItem contentAreaItem) => this.GetContent(contentAreaItem, this._contextModeResolver.CurrentMode);


        public DisplayOption LoadDisplayOption(ContentAreaItem contentAreaItem)
        {
            return null;
        }
       
        private IContentData Load(ContentAreaItem item, ContextMode contextMode)
        {
            if (item.InlineBlock != null)
                return (IContentData)item.InlineBlock;

            var content = this.GetContent(item, contextMode);
            return content;
            //return contextMode != ContextMode.Preview || !this.IsMasterFallback(content, language) ? (IContentData)content : (IContentData)null;
        }

        private IContent GetContent(ContentAreaItem item, ContextMode contextMode)
        {
            var httpContextMode = _contentHelper.GetContextMode();

            if (httpContextMode == ContextMode.Edit || httpContextMode == ContextMode.Preview)
            {
                return _contentHelper.GetDraftContent<IContent>(item.ContentLink);
            }
            return _contentHelper.GetContent<IContent>(item.ContentLink);
        }
       
    }
}

