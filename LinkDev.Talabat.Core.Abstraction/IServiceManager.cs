using LinkDev.Talabat.Core.Abstraction.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
