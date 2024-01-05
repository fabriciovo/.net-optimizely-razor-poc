using razor_poc.Models.Blocks;

namespace razor_poc.Models.ViewModels
{
    public class TextBlockViewModel : BaseBlockViewModel<TextBlock>
    {
        public TextBlockViewModel(TextBlock currentBlock) : base(currentBlock)
        {
            Text = currentBlock?.Contents?.ToHtmlString();
        }

        public string Text { get; set; }
    }
}
