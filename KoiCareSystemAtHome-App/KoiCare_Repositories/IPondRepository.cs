using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public interface IPondRepository
    {
        public List<PondsTbl> GetAllPonds();
        public List<PondsTbl> GetPondsByUserId(int userId);
        public PondsTbl GetPondById(int id);
        public PondsTbl GetPondByName(string name);
        public bool CreatePond(PondsTbl pond);
        public bool UpdatePond(PondsTbl pond);
        public bool DeletePond(int id);
    }
}
