using System.Collections.ObjectModel;
using vega.ViewModels;

namespace vega.Models.ViewModels
{
    public class MakeViewModel : KeyValuePairViewModel
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //these can be removed because they are already included by making MakeViewModel derived from KeyValuePairViewModel
        public ICollection<KeyValuePairViewModel> Models { get; set; }

        public MakeViewModel() 
        {
            Models = new Collection<KeyValuePairViewModel>();
        }
    }
}
