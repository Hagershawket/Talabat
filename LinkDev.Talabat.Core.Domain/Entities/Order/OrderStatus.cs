using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Order
{
    public enum OrderStatus
    {
        Pending = 1,
        PaymentReceived = 2,
        PaymentFailed = 3,
    }
}
