using EPiServer.SpecializedProperties;
using System.ComponentModel.DataAnnotations;
using razor_poc.Models.Blocks;

namespace razor_poc.Models.ViewModels
{
    public class HeroBlockViewModel : BaseBlockViewModel<HeroBlock>
    {
        public HeroBlockViewModel(HeroBlock currentBlock) : base(currentBlock)
        {
            Title = currentBlock.Title;
            Subtitle = currentBlock.Subtitle;
            Description = currentBlock.Description?.ToHtmlString();
            CtaLink = new LinkItemViewModel(currentBlock.CtaLink);
            HeroImage = new ImageViewModel(currentBlock.HeroImage);
        }
       
        public virtual string Title { get; set; }        
        public virtual string Subtitle { get; set; }
        public virtual string Description { get; set; }     
        public virtual LinkItemViewModel CtaLink { get; set; }       
        public virtual ImageViewModel HeroImage { get; set; }

    }
}
