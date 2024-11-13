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
    public class WaterParameterRepo : IWaterParameterRepo
    {
        public bool AddParameter(WaterParametersTbl water)
        {
            return WaterDAO.Instance.AddParameter(water);
        }

        public bool DeleteParameter(int id)
        {
            return WaterDAO.Instance.DeleteParameter(id);
        }

        public List<WaterParametersTbl> GetAllParameters()
        {
            return WaterDAO.Instance.GetAllParameters();
        }

        public WaterParametersTbl GetParameterById(int id)
        {
            return WaterDAO.Instance.GetParameterById(id);
        }

        public WaterParametersTbl GetParameterByName(string name)
        {
            return WaterDAO.Instance.GetParameterByName(name);
        }

        public List<WaterParametersTbl> GetParametersByUserId(int userId)
        {
            return WaterDAO.Instance.GetParametersByUserId(userId);
        }

        public bool UpdateParameter(WaterParametersTbl water)
        {
            return WaterDAO.Instance.UpdateParameter(water);
        }
    }
}