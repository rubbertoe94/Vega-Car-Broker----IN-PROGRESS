using System.ComponentModel.DataAnnotations;

namespace vega.Models.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Phone { get; set; }
    }
}
