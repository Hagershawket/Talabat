namespace LinkDev.Talabat.Core.Domain.Entities.Order
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public required string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }
    }
}
