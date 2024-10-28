using Business_Object.Models;
using KoiCare_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class PondRepository : IPondRepository
    {
        public bool CreatePond(PondsTbl pond)
            =>PondDAO.Instance.AddPond(pond);

        public bool DeletePond(int id)
            =>PondDAO.Instance.DeletePond(id);

        public List<PondsTbl> GetAllPonds()
            => PondDAO.Instance.GetAllPonds();

        public PondsTbl GetPondById(int id)
            => PondDAO.Instance.GetPondById(id);

        public PondsTbl GetPondByName(string name)
            => PondDAO.Instance.GetPondByName(name);

        public List<PondsTbl> GetPondsByUserId(int userId)
            => PondDAO.Instance.GetPondsByUserId(userId);

        public bool UpdatePond(PondsTbl pond)
            => PondDAO.Instance.UpdatePond(pond);
    }
}
