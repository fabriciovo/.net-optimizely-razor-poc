using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;
using razor_poc.Infrastructure.Constants;

namespace razor_poc.Models.Blocks
{
    [ContentType(
     GUID = "9D73C3F9-5A4C-42DD-91C3-94A782CFB52D",
     DisplayName = "3D Hero Block",
        GroupName = GroupNames.ContentBlocks)]
    public class Hero3DBlock : BaseBlock
    {
        [Display(
          Name = "3D Hero Title",
          GroupName = SystemTabNames.Content,
          Order = 10)]
        [Required]
        [CultureSpecific]
        public virtual string Title { get; set; }

        [Display(Name = "3D Hero Model",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        [CultureSpecific]
        [Required]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Model3D { get; set; }

    }
}
