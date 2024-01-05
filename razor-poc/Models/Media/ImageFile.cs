using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace razor_poc.Models.Media
{
    [ContentType(DisplayName = "Image File",
         GUID = "913A2168-0C4F-4DA2-B547-7C4A6C262B9A",
         Description = "Used for image file types such as jpg, jpeg, jpe, ico, gif, bmp, png,svg,eps")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png,tif,tiff,svg,eps")]
    public class ImageFile : ImageData, IMediaContent
    {
        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 150)]
        public virtual string Title { get; set; }
      
        [CultureSpecific]
        [Display(Name = "Alternate text", GroupName = SystemTabNames.Content, Order = 170)]
        public virtual string AltText { get; set; }

        [Editable(false)]
        public override Blob Thumbnail { get => BinaryData; }
    }
}
