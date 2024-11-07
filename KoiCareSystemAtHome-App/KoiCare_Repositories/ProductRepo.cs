using Business_Object.Models;
using KoiCare_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class ProductRepo : IProductRepo
    {
        public List<ProductsTbl> GetProducts() => ProductDAO.Instance.GetProducts();
        
    }
}
