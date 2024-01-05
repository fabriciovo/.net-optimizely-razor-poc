using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using razor_poc.Infrastructure.Constants;
using razor_poc.Models.ViewModels;

namespace razor_poc.Models.Pages
{
    /// <summary>
    /// Base class for all page types
    /// </summary>
    public abstract class BasePage : PageData
    {
        [Display(
            GroupName = GroupNames.Seo,
            Order = 100)]
        [CultureSpecific]
        public virtual string Title
        {
            get
            {
                var pageTitle = this.GetPropertyValue(p => p.Title);

                // Use explicitly set meta title, otherwise fall back to page name
                return !string.IsNullOrWhiteSpace(pageTitle)
                       ? pageTitle
                       : PageName;
            }
            set => this.SetPropertyValue(p => p.Title, value);
        }


        [Display(
            GroupName = GroupNames.Seo,
            Order = 100)]
        [CultureSpecific]
        public virtual string MetaTitle
        {
            get
            {
                var metaTitle = this.GetPropertyValue(p => p.MetaTitle);

                // Use explicitly set meta title, otherwise fall back to page name
                return !string.IsNullOrWhiteSpace(metaTitle)
                       ? metaTitle
                       : PageName;
            }
            set => this.SetPropertyValue(p => p.MetaTitle, value);
        }

        [Display(
            GroupName = GroupNames.Seo,
            Order = 200)]
        [CultureSpecific]
        [BackingType(typeof(PropertyStringList))]
        public virtual IList<string> MetaKeywords { get; set; }

        [Display(
            GroupName = GroupNames.Seo,
            Order = 300)]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        public virtual string MetaDescription { get; set; }

        [Ignore]
        public bool OpenInNewWindow => this.LinkType != PageShortcutType.Normal && this.TargetFrameName == "_blank";
        
        public static IEnumerable<ComponentView> GetPageComponents()
        {
            var httpContextAccessor = ServiceLocator.Current.GetInstance<IHttpContextAccessor>();
            var components = httpContextAccessor.HttpContext.Items["ComponentsOnPage"] as List<ComponentView> ?? new List<ComponentView>();
            return components;
        }
    }
}