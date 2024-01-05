using EPiServer.DataAbstraction.Internal;
using EPiServer.Shell.ViewComposition;
using EPiServer.SpecializedProperties;
using System.ComponentModel.DataAnnotations;
using razor_poc.Infrastructure.Constants;
using razor_poc.Models.Blocks;

namespace razor_poc.Models.Pages
{
    [ContentType(DisplayName = "Home Page",
          GUID = "76FC6353-B517-4FEA-ADC0-7A5871709A0D",
          GroupName = GroupNames.ContentPages)]   
    [AvailableContentTypes(
          Availability.Specific,
          Include = new[]
          {
            typeof(StandardPage),           
            typeof(ContentFolder) }, // Pages we can create under the home page...
          ExcludeOn = new[]
          {       
            typeof(StandardPage),
       
          })] // ...and underneath those we can't create additional home pages
    public class HomePage : BasePage, IHomePage
    {
       
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [CultureSpecific]
        [AllowedTypes(typeof(HeroBlock))]       
        public virtual ContentArea HeroBlockContentArea { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 150)]
        [CultureSpecific]
        [AllowedTypes(typeof(TextBlock))]
        public virtual ContentArea TextBlockContentArea { get; set; }

        [Display(
           GroupName = SystemTabNames.Settings,
           Order = 200)]
        [CultureSpecific]
        public virtual PageReference SiteSettingsPageLink { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 250)]
        [CultureSpecific]
        [AllowedTypes(typeof(Hero3DBlock))]
        public virtual ContentArea Hero3DBlockContentArea { get; set; }
    }
}
