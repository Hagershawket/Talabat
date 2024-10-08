using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> getRepository<TEntity, TKey>() 
            where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>;

        Task<int> CompleteAysnc();
    }
}
