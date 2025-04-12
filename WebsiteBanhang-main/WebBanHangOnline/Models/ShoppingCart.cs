using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }

        public void AddToCart(ShoppingCartItem item, int quantity)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProductId == item.ProductId && x.Fragrance == item.Fragrance);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.TotalPrice = existingItem.Price * existingItem.Quantity;
            }
            else
            {
                item.TotalPrice = item.Price * item.Quantity;
                Items.Add(item);
            }
        }


        // Thứ tự và kiểu chính xác
        public void UpdateQuantity(int productId, string fragrance, int quantity)
        {
            var item = Items.FirstOrDefault(x =>
                x.ProductId == productId &&
                string.Equals(x.Fragrance ?? "", fragrance ?? "", StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                item.Quantity = quantity;
                item.TotalPrice = item.Price * quantity;
            }
        }

        public void Remove(int id, string fragrance)
        {
            var item = Items.FirstOrDefault(x =>
                x.ProductId == id &&
                string.Equals(x.Fragrance ?? "", fragrance ?? "", StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(x=>x.TotalPrice);
        }
        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            Items.Clear();
        }

    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Fragrance { get; set; }
    }
}