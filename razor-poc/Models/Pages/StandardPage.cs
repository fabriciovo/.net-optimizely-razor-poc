using System.ComponentModel.DataAnnotations;
using razor_poc.Infrastructure.Constants;

namespace razor_poc.Models.Pages
{
    [ContentType(
        DisplayName = "Standard Page",
        GUID = "EA006084-A065-4CCF-8B44-9D9A369C8B3A",
        GroupName = GroupNames.ContentPages)]
    public class StandardPage : BasePage
    {
        [Display(
          GroupName = SystemTabNames.Content,
          Order = 100)]
        [CultureSpecific]
        public virtual ContentArea Contents { get; set; }
    }
}
