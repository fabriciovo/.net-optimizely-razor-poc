using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;
using razor_poc.Infrastructure.Constants;

namespace razor_poc.Models.Blocks
{
    [ContentType(
     GUID = "9D73C3F9-5A4C-42DD-91C3-94A782EEB52C",
     DisplayName = "Hero Block",
        GroupName = GroupNames.ContentBlocks)]
    public class HeroBlock : BaseBlock
    {
        [Display(
          Name = "Hero Title",
          GroupName = SystemTabNames.Content,
          Order = 10)]
        [Required]
        [CultureSpecific]
        public virtual string Title { get; set; }

        [Display(
         Name = "Subtitle",
         GroupName = SystemTabNames.Content,
         Order = 15)]

        [CultureSpecific]
        [MaxLength(100)]
        public virtual string Subtitle { get; set; }

        [Display(
            Name = "Intro Text",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [CultureSpecific]
        public virtual XhtmlString Description { get; set; }

        [Display(Name = "CTA",
        GroupName = SystemTabNames.Content,
        Order = 30)]
        [CultureSpecific]
        public virtual LinkItem CtaLink { get; set; }

       
        [Display(Name = "Hero Image",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        [CultureSpecific]
        [Required]
        [UIHint(UIHint.Image)]
        public virtual ContentReference HeroImage { get; set; }

    }
}
