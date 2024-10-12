using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        // This Object is Created via this Constructor, Will be Used for Building the Query that Get all Products
        public ProductWithBrandAndCategorySpecifications(string? sort)
            : base()
        {
            AddIncludes();

            AddOrderBy(P => P.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(P => P.Name);
                        break;
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
        }

        // This Object is Created via this Constructor, Will be Used for Building the Query that Get a Specific Product
        public ProductWithBrandAndCategorySpecifications(int Id)
            : base(Id)
        {
            AddIncludes();
        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
