using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;
using razor_poc.Infrastructure.Constants;

namespace razor_poc.Models.Pages
{
    [ContentType(
       GUID = "7D848BCB-E216-4C91-A80D-E03C52219887",
       GroupName = GroupNames.ContentPages)]
    public class SiteSettingsPage : PageData
    {
        [Display(
        GroupName = SystemTabNames.Content,
        Order = 50)]
        [CultureSpecific]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Favicon { get; set; }

        [Display(
         GroupName = SystemTabNames.Content,
         Order = 100)]
        [CultureSpecific]
        [UIHint(UIHint.Image)]               
        public virtual ContentReference SiteLogo { get; set; }

        [Display(
         GroupName = SystemTabNames.Content,
        Order = 200)]
        [CultureSpecific]        
        public virtual string FooterLinksTitle { get; set; }

        [Display(
        GroupName = SystemTabNames.Content,
        Order = 300)]
        [CultureSpecific]
        public virtual LinkItemCollection FooterLinks { get; set; }


        [Display(
         GroupName = SystemTabNames.Content,
        Order = 400)]
        [CultureSpecific]
        public virtual string CopyrightText { get; set; }

    }
}
