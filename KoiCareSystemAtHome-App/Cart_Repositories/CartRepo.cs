using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_Repositories
{
    public class CartRepo : ICartRepo
    {
        public void AddProductToCart(int accId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public List<CartTbl> GetCartItems(int accId)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductFromCart(int accId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
