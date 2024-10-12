using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal static class SpecificationsEvaluator<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery; // _dbContext.Set<Product>()

            if (spec.Criteria is not null) // P => P.Id == 10
                query = query.Where(spec.Criteria);          // query = _dbContext.Set<Product>().Where(P => P.Id == 10);


            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            else if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);         // query = _dbContext.Set<Product>().OrderBy(P => P.Name)

            // include expressions
            // 1. P => P.Brand
            // 2. P => P.Category

            query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            // query = _dbContext.Set<Product>().Where(P => P.Id == 10).Include(P => P.Brand)
            // query = _dbContext.Set<Product>().Where(P => P.Id == 10).Include(P => P.Brand.Include(P => P.Category)

            return query;
        }
    }
}
