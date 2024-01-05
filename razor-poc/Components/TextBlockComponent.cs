using EPiServer.Data;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using razor_poc.Models.Blocks;
using razor_poc.Models.ViewModels;

namespace razor_poc.Components
{
    public class TextBlockComponent : BaseBlockComponent<TextBlock>
    {
        protected override IViewComponentResult InvokeComponent(TextBlock currentContent)
        {
            var data = new TextBlockViewModel(currentContent);
            return BuildView(data);
        }
    }
}
