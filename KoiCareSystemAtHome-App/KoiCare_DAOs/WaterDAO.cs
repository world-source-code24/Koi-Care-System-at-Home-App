using Business_Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class WaterDAO
    {
        private readonly KoiCareSystemAppContext _context;
        private static WaterDAO instance;
        public static WaterDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WaterDAO();
                }
                return instance;
            }
        }
        public WaterDAO()
        {
            _context = new KoiCareSystemAppContext();
        }

        public List<WaterParametersTbl> GetAllParameters()
        {
            return _context.WaterParametersTbls.ToList();
        }
        public WaterParametersTbl GetParameterById(int id)
        {
            return _context.WaterParametersTbls.Where(w => w.ParameterId == id).SingleOrDefault();
        }
        public List<WaterParametersTbl> GetParametersByUserId(int userId)
        {
            return _context.WaterParametersTbls.Include(w => w.Pond).Where(w => w.Pond.AccId == userId).ToList();
        }
        public WaterParametersTbl GetParameterByName(string name)
        {
            return _context.WaterParametersTbls.Where(w => w.Pond.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        }

        public bool AddParameter(WaterParametersTbl water)
        {
            bool isSuccess = true;
            try
            {
                _context.WaterParametersTbls.Add(water);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool UpdateParameter(WaterParametersTbl water)
        {
            var updateWater = GetParameterById(water.ParameterId);
            if (updateWater != null)
            {
                _context.Entry<WaterParametersTbl>(updateWater).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteParameter(int id)
        {
            var deleteWater = GetParameterById(id);
            if (deleteWater != null)
            {
                _context.WaterParametersTbls.Remove(deleteWater);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
