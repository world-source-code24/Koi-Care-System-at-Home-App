using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class ProductDAO
    {
        private KoicareathomeContext _context;
        private static ProductDAO instance = null;

        public ProductDAO()
        {
            _context = new KoicareathomeContext();
        }

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
        }

        public List<ProductsTbl> GetProducts()
        {
            return _context.ProductsTbls.ToList();
        }




    }
}
