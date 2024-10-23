using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.ViewModels.Brand
{
    public class BrandViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Display(Name = "Created Date")]
        public string? CreatedOn { get; set; }

        [Display(Name = "Last Modified Date")]
        public string? LastModifiedOn { get; set; }
    }
}
