using EPiServer.SpecializedProperties;
using razor_poc.Infrastructure.Extensions;
using razor_poc.Models.Media;

namespace razor_poc.Models.ViewModels
{
    public class Model3DViewModel
    {
        public Model3DViewModel(ContentReference model3D)
        {
            if (model3D == null) return;

            var content = model3D.GetContent<Model3DFile>();
            Href = model3D.GetFriendlyUrl();
            Text = content?.Title??content?.Name;
            AltText = content?.AltText;            
        }
        public string Href { get; set; }
        public string Text { get; set; }
        public string AltText { get; set; }       
    }
}
