using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base
{
    [DbContextType(typeof(StoreDbContext))]
    internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity> 
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id)
                   .ValueGeneratedOnAdd();           
        }
    }
}
