﻿using LinkDev.Talabat.Core.Abstraction.Models.Common;

namespace LinkDev.Talabat.Core.Abstraction.Models.Orders
{
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public required AddressDto ShippingAddress { get; set; }
    }
}
