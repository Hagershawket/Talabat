namespace LinkDev.Talabat.Dashboard.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; }

        public string? PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

        public string? Category { get; set; }

        public int? BrandId { get; set; }

        public string? Brand { get; set; }

    }
}
