using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_Repositories
{
    public interface ICartRepo
    {
        public List<CartTbl> GetCartItems(int accId);
        public void AddProductToCart(int accId, int productId, int quantity);
        public void RemoveProductFromCart(int accId, int productId);
    }
}
