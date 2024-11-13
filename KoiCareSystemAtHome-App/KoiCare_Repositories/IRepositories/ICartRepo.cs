using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories.IRepositories
{
    public interface ICartRepo
    {
        public List<CartTbl> GetCartItems(int accId);
        public bool AddProductToCart(int accId, int productId, int quantity);
        public bool RemoveProductFromCart(int accId, int productId);
    }
}