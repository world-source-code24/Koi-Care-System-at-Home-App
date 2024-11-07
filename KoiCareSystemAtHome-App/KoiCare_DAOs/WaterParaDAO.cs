using Business_Object;
using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class WaterParaDAO
    {
        private KoicareathomeContext _context;
        private static WaterParaDAO instance;

        public WaterParaDAO()
        {
            _context = new KoicareathomeContext();
        }

        public static WaterParaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WaterParaDAO();
                }
                return instance;
            }
        }
        public List<WaterParametersTbl> GetWaterParameter()
        {
            return _context.WaterParametersTbls.ToList();
        }
        public List<WaterParametersTbl> GetWaterParaByAccId(int accId)
        {
            var pondId = _context.PondsTbls.Where(pond => pond.PondId == accId).Select(pond => pond.PondId).ToList();
            return _context.WaterParametersTbls.Where(water => pondId.Contains(water.PondId)).ToList();
        }
        public WaterParametersTbl GetWaterDetailById(int id)
        {
            return _context.WaterParametersTbls.SingleOrDefault(water => water.ParameterId == id);
        }
        public bool CreateWaterParameter(WaterParametersTbl waterPara)
        {
            bool result = true;
            try
            {
                _context.WaterParametersTbls.Add(waterPara);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool UpdateWaterParameter(WaterParametersTbl waterPara)
        {
            var updateWaterPara = GetWaterDetailById(waterPara.ParameterId);
            if (updateWaterPara != null)
            {
                updateWaterPara.Temperature = waterPara.Temperature;
                updateWaterPara.Salt = waterPara.Salt;
                updateWaterPara.PhLevel = waterPara.PhLevel;
                updateWaterPara.O2Level = waterPara.O2Level;
                updateWaterPara.No2Level = waterPara.No2Level;
                updateWaterPara.No3Level = waterPara.No3Level;
                updateWaterPara.Po4Level = waterPara.Po4Level;
                updateWaterPara.TotalChlorines = waterPara.TotalChlorines;
                updateWaterPara.Date = waterPara.Date;
                updateWaterPara.Note = waterPara.Note;
                updateWaterPara.PondId = waterPara.PondId;
                _context.WaterParametersTbls.Update(updateWaterPara);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteWaterParameter(int id)
        {
            var deleteWaterPara = GetWaterDetailById(id);
            if (deleteWaterPara != null)
            {
                _context.WaterParametersTbls.Remove(deleteWaterPara);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
