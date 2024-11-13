using Business_Object.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class CartDAO
    {
        private KoicareathomeContext _context;
        private static CartDAO instance = null;


        public CartDAO()
        {
            _context = new KoicareathomeContext();
        }

        public static CartDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CartDAO();
                }
                return instance;
            }
        }

        public CartTbl GetCartByAccountId(int id)
        {
            return _context.CartTbls.SingleOrDefault(c => c.AccId.Equals(id));
        }


        public List<CartTbl> GetCartItems(int accId)
        {


            return _context.CartTbls.Include(c => c.Product)
                .Where(c => c.AccId == accId).ToList();
        }

        public bool AddToCart(int accId, int productId, int quantity)
        {
            var isExisted = _context.CartTbls.SingleOrDefault(c => c.AccId == accId && c.ProductId == productId);

            if (isExisted != null)
            {
                isExisted.Quantity++;
            }
            else
            {
                var newCartItem = new CartTbl
                {
                    AccId = accId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartTbls.Add(newCartItem);
            }
            _context.SaveChanges();
            return true;
        }

        public bool RemoveFromCart(int accId, int productId)
        {
            var item = _context.CartTbls.SingleOrDefault(c => c.AccId == accId && c.ProductId == productId);

            if (item != null)
            {
                _context.CartTbls.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

}