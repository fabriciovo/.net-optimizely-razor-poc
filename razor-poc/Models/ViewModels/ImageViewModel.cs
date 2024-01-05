using EPiServer.SpecializedProperties;
using razor_poc.Infrastructure.Extensions;
using razor_poc.Models.Media;

namespace razor_poc.Models.ViewModels
{
    public class ImageViewModel
    {
        public ImageViewModel(ContentReference image)
        {
            if (image == null) return;

            var content = image.GetContent<ImageFile>();
            Href = image.GetFriendlyUrl();
            Text = content?.Title??content?.Name;
            AltText = content?.AltText;            
        }
        public string Href { get; set; }
        public string Text { get; set; }
        public string AltText { get; set; }       
    }
}
