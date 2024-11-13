using Business_Object.Models;
using KoiCare_DAOs;
using KoiCare_Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class CartRepo : ICartRepo
    {
        public bool AddProductToCart(int accId, int productId, int quantity)
            => CartDAO.Instance.AddToCart(accId, productId, quantity);

        public List<CartTbl> GetCartItems(int accId)
            => CartDAO.Instance.GetCartItems(accId);

        public bool RemoveProductFromCart(int accId, int productId)
            => CartDAO.Instance.RemoveFromCart(accId, productId);
    }
}

