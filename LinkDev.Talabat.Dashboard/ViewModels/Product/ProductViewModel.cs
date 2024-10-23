using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; }

        public string? PictureUrl { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Display(Name = "Categories")]
        public int? CategoryId { get; set; }

        public string? Category { get; set; }

        [Display(Name = "Brands")]
        public int? BrandId { get; set; }

        public string? Brand { get; set; }

        [Display(Name = "Created Date")]
        public string? CreatedOn { get; set; }

        [Display(Name = "Last Modified Date")]
        public string? LastModifiedOn { get; set; }

    }
}
