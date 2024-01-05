using System.ComponentModel.DataAnnotations;

namespace razor_poc.Infrastructure.Constants
{
    [GroupDefinitions]
    public static class GroupNames
    {
        [Display(Name = "SEO", Order = 10)]
        public const string Seo = "SEO";

        [Display(Name = "Pages", Order = 10)]
        public const string ContentPages = "Content Pages";

        [Display(Name = "SiteSettings", Order = 50)]
        public const string SiteSettings = "SiteSettings";

        [Display(Name = "ContentBlocks", Order = 100)]
        public const string ContentBlocks = "Content Blocks";

    }
}
