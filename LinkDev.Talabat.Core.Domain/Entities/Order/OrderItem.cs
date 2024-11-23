﻿namespace LinkDev.Talabat.Core.Domain.Entities.Order
{
    public class OrderItem : BaseAuditableEntity<int>
    {
        public required ProductItemOrdered Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
