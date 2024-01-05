using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace razor_poc.Models.Media
{
    [ContentType(DisplayName = "Video File",
        GUID = "1474033C-9AFC-4E3C-B6D2-1AAA1520025A",
        Description = "Used for video file types such as mp4, flv, webm")]
    [MediaDescriptor(ExtensionString = "mp4,flv,webm")]
    public class VideoFile : VideoData, IMediaContent
    {
        [Display(GroupName = SystemTabNames.Content, Order = 20)]
        public virtual string Title { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 20)]
        [UIHint(UIHint.Textarea)]
        public virtual string Description { get; set; }

        [CultureSpecific]
        [Display(Name = "Alternate text", GroupName = SystemTabNames.Content, Order = 170)]
        public virtual string AltText { get; set; }

        [UIHint(UIHint.Image)]
        [Display(Name = "Preview image", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual ContentReference PreviewImage { get; set; }

    }
}
