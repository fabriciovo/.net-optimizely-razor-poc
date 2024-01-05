namespace razor_poc.Models.Pages
{
    public interface IHomePage : IContent
    {
        public PageReference SiteSettingsPageLink { get; set; }
    }
}
