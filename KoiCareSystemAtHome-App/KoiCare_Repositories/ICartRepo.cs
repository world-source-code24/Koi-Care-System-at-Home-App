using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public interface ICartRepo
    {
        public bool AddToCart(int accId, int productId);

        public List<CartTbl> GetCartItems(int accId);

        public bool RemoveFromCart(int accId, int productId);


        public bool ClearCart(int accId);
    }
}
