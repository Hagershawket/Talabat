using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Products
{
    public class Product : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        private string normalizedName = null!;
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; }
        public virtual ProductBrand? Brand { get; set; }
        public int? CategoryId { get; set; }
        public virtual ProductCategory? Category { get; set; }

        public required string NormalizedName
        {
            get { return normalizedName; }
            set { normalizedName = Name.ToUpper(); }
        }

    }
}
