using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace razor_poc.Models.Media
{
    [ContentType(DisplayName = "3D Model File",
         GUID = "913A2168-0C4F-4DA2-B547-7C4A6C262C3D",
         Description = "Used for 3D file types such as glb, gltf")]
    [MediaDescriptor(ExtensionString = "glb,gltf")]
    public class Model3DFile : ImageData, IMediaContent
    {
        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 150)]
        public virtual string Title { get; set; }
      
        [CultureSpecific]
        [Display(Name = "Alternate text", GroupName = SystemTabNames.Content, Order = 170)]
        public virtual string AltText { get; set; }

        [UIHint(UIHint.Image)]
        [Display(Name = "Preview image", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual ContentReference PreviewImage { get; set; }

    }
}
