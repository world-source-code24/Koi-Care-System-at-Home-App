using Business_Object.Models;
using KoiCare_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class WaterParaRepo : IWaterParaRepo
    {
        public bool CreateWaterParameter(WaterParametersTbl waterPara)
            => WaterParaDAO.Instance.CreateWaterParameter(waterPara);
        public bool DeleteWaterParameter(int id)
            => WaterParaDAO.Instance.DeleteWaterParameter(id);

        public List<WaterParametersTbl> GetWaterParaByAccId(int accId)
            => WaterParaDAO.Instance.GetWaterParaByAccId(accId);

        public WaterParametersTbl GetWaterDetailById(int id)
            => WaterParaDAO.Instance.GetWaterDetailById(id);

        public List<WaterParametersTbl> GetWaterParameter()
            => WaterParaDAO.Instance.GetWaterParameter();

        public bool UpdateWaterParameter(WaterParametersTbl waterPara)
            => WaterParaDAO.Instance.UpdateWaterParameter(waterPara);
    }
}
