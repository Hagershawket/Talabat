using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<Product, int> ProductRepository { get; }
        public IGenericRepository<ProductBrand, int> BrandRepository { get; }
        public IGenericRepository<ProductCategory, int> CategoryRepository { get; }

        Task<int> CompleteAysnc();
    }
}
