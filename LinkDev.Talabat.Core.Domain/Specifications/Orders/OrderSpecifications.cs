using LinkDev.Talabat.Core.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order, int>
    {
        public OrderSpecifications(string buyerEmail) 
            : base(order => order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
            AddOrderByDesc(order => order.OrderDate);
        }

        public OrderSpecifications(string buyerEmail, int orderId) 
            : base(order => order.Id == orderId && order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
        }

        private OrderSpecifications(Expression<Func<Order, bool>> criteria)
        : base(criteria)
        {
        }
        public static OrderSpecifications ForPaymentIntentId(string paymentIntentId)
        {
            return new OrderSpecifications(order => order.PaymentIntentId == paymentIntentId);
        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(Order => Order.Items);
            Includes.Add(Order => Order.DeliveryMethod!);
        }
    }
}
