using BlazorAppDemo2022.Components.Model;

namespace BlazorAppDemo2022.Components.Service
{
    public class CartService
    {
        public List<Product> Items { get; set; } = new();

        public void Add(Product product)
        {
            Items.Add(product);
        }
    }
}