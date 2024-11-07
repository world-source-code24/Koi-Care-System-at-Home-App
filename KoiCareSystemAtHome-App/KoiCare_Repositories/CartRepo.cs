using Business_Object.Models;
using KoiCare_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class CartRepo : ICartRepo
    {

        public bool AddToCart(int accId, int productId) => CartDAO.Instance.AddToCart(accId, productId);
        

        public List<CartTbl> GetCartItems(int accId) => CartDAO.Instance.GetCartItems(accId);
        

        public bool RemoveFromCart(int accId, int productId) => CartDAO.Instance.RemoveFromCart(accId, productId);


        
    }
}
