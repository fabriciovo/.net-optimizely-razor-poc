using EPiServer.SpecializedProperties;
using System.ComponentModel.DataAnnotations;
using razor_poc.Models.Blocks;
using razor_poc.Models.Media;

namespace razor_poc.Models.ViewModels
{
    public class Hero3DBlockViewModel : BaseBlockViewModel<Hero3DBlock>
    {
        public Hero3DBlockViewModel(Hero3DBlock currentBlock) : base(currentBlock)
        {
            Title = currentBlock.Title;
            Model3D = new Model3DViewModel(currentBlock.Model3D);
        }

        public virtual string Title { get; set; }
        public virtual Model3DViewModel Model3D { get; set; }

    }
}
