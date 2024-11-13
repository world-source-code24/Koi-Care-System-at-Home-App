using Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public interface IWaterParameterRepo
    {
        public List<WaterParametersTbl> GetAllParameters();
        public WaterParametersTbl GetParameterById(int id);
        public List<WaterParametersTbl> GetParametersByUserId(int userId);
        public WaterParametersTbl GetParameterByName(string name);
        public bool AddParameter(WaterParametersTbl water);
        public bool UpdateParameter(WaterParametersTbl water);
        public bool DeleteParameter(int id);
    }
}
