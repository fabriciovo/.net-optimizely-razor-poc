using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace razor_poc.Models.ViewModels
{
    public class SiteSettingsViewModel
    {
       
        public ImageViewModel Favicon { get; set; }        
        public ImageViewModel SiteLogo { get; set; }       
        public string FooterLinksTitle { get; set; }        
        public IEnumerable<LinkItemViewModel> FooterLinks { get; set; }        
        public  string CopyrightText { get; set; }
    }
}
