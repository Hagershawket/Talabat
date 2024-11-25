using LinkDev.Talabat.Core.Domain.Entities.Order;
using LinkDev.Talabat.Infrastructure.Persistence._Data.Config.Base;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Data.Config.Orders
{
    internal class OrderItemConfigurations : BaseAuditableEntityConfigurations<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(item => item.Product, product => product.WithOwner());

            builder.Property(item => item.Price)
                   .HasColumnType("decimal(8, 2)");
        }
    }
}
