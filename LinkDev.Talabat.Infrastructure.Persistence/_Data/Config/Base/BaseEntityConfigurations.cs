﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base
{
    [DbContextType(typeof(StoreDbContext))]
    internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity> 
        where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id)
                   .UseIdentityColumn(1, 1);

            builder.Property(E => E.CreatedBy)
                   .IsRequired();

            builder.Property(E => E.CreatedOn)
                   .IsRequired();

            builder.Property(E => E.LastModifiedBy)
                   .IsRequired();

            builder.Property(E => E.LastModifiedOn)
                   .IsRequired();
        }
    }
}
