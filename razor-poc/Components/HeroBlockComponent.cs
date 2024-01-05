using Azure.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using razor_poc.Models.Blocks;
using razor_poc.Models.ViewModels;

namespace razor_poc.Components
{
    public class HeroBlockComponent : BaseBlockComponent<HeroBlock>
    {
        protected override IViewComponentResult InvokeComponent(HeroBlock currentContent)
        {
            var data = new HeroBlockViewModel(currentContent);
            return BuildView(data);
        }
    }
}
