namespace LinkDev.Talabat.Core.Domain.Entities.Order
{
    public class ProductItemOrdered
    {
        public required int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
    }
}
