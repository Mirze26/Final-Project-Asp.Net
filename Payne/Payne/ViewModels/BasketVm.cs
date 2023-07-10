using Payne.Models;

namespace Payne.ViewModels
{
    public class BasketVm
    {
        public List<BasketItem> BasketItems { get; set; }
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }
    }
}
