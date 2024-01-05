using System.Text.Json.Serialization;

namespace razor_poc.Models.ViewModels
{
    public abstract class BaseBlockViewModel<T> where T: BlockData
    {
        public BaseBlockViewModel(T currentBlock)
        {
            ComponentName = currentBlock.GetOriginalType().Name;
            Id = $"{(currentBlock as IContent)?.ContentGuid ?? Guid.NewGuid()}";
            //CurrentBlock = currentBlock;
        }

        //[JsonIgnore]       
        //public T CurrentBlock { get; set; }

        public string ComponentName { get; private set; }
        public string Id { get; set; }
    }
}
