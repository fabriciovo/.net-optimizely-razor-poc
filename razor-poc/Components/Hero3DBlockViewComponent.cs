using Azure.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using razor_poc.Models.Blocks;
using razor_poc.Models.ViewModels;

namespace razor_poc.Components
{
    public class Hero3DBlockViewComponent : BaseBlockComponent<Hero3DBlock>
    {
        protected override IViewComponentResult InvokeComponent(Hero3DBlock currentContent)
        {
            var data = new Hero3DBlockViewModel(currentContent);
            return BuildView(data);
        }
    }
}
