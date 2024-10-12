using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        // This Object is Created via this Constructor, Will be Used for Building the Query that Get all Products
        public ProductWithBrandAndCategorySpecifications()
            : base()
        {
            AddIncludes();
        }

        // This Object is Created via this Constructor, Will be Used for Building the Query that Get a Specific Product
        public ProductWithBrandAndCategorySpecifications(int Id)
            : base(Id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
