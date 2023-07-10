namespace Payne.Models
{
    public class Product:BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int Rating { get; set; }
        public string Sku { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }







    }
}
