using Microsoft.CodeAnalysis.FlowAnalysis;
using System.ComponentModel.DataAnnotations;
using razor_poc.Infrastructure.Constants;

namespace razor_poc.Models.Blocks
{
    [ContentType(
      GUID = "34564B90-6C6E-4E73-9CCC-48BF7C2F4F27",
      DisplayName = "Rich Text Block",
         GroupName = GroupNames.ContentBlocks)]
    public class TextBlock : BaseBlock
    {
        [Display(Name = "Text",
           GroupName = SystemTabNames.Content,
           Order = 10)]
        [CultureSpecific]
        [Required]
        public virtual XhtmlString Contents { get; set; }
    }
}
