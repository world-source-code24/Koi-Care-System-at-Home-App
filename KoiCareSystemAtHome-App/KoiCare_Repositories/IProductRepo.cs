using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public interface IProductRepo
    {
        public List<ProductsTbl> GetProducts();

        //public addProductToCart 
    }
}
