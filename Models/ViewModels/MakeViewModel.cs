namespace vega.Models.ViewModels
{
    public class MakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
