using EPiServer.SpecializedProperties;
using razor_poc.Infrastructure.Extensions;

namespace razor_poc.Models.ViewModels
{
    public class LinkItemViewModel
    {        
        public LinkItemViewModel(LinkItem item) {
            if (item == null)
                return;

            Href = item.Href.GetFriendlyUrl();
            Title = item.Title;
            Text = item.Text;
            Target = item.Target;
        }
        public string Href { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
    }
}
