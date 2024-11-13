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
    public class ProductRepository : IProductRepository
    {
        public List<ProductsTbl> GetAllProducts()
            => ProductDAO.Instance.GetProducts();
    }
}
