using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Services
{
    public interface IWaterParaService
    {
        public WaterParametersTbl GetWaterDetailById(int id);
        public List<WaterParametersTbl> GetWaterParaByAccId(int accId);
        public List<WaterParametersTbl> GetWaterParameter();
        public bool CreateWaterParameter(WaterParametersTbl waterPara);
        public bool UpdateWaterParameter(WaterParametersTbl waterPara);
        public bool DeleteWaterParameter(int id);
    }
}
