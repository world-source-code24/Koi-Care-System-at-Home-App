using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories.IRepositories
{
    public interface IProductRepository
    {
        List<ProductsTbl> GetAllProducts();
    }
}
