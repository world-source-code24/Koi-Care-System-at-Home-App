using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class PondDAO
    {
        private readonly KoicareathomeContext _context;
        private static PondDAO instance;
        public static PondDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PondDAO();
                }
                return instance;
            }
        }
        public PondDAO()
        {
            _context = new KoicareathomeContext();
        }

        public List<PondsTbl> GetAllPonds()
        {
            return _context.PondsTbls.ToList();
        }
        public PondsTbl GetPondById(int id)
        {
            return _context.PondsTbls.Where(p => p.PondId == id).SingleOrDefault();
        }
        public List<PondsTbl> GetPondsByUserId(int userId)
        {
            return _context.PondsTbls.Where(p => p.AccId == userId).ToList();
        }
        public PondsTbl GetPondByName(string name)
        {
            return _context.PondsTbls.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        }

        public bool AddPond(PondsTbl pond)
        {
            bool isSuccess = true;
            try
            {
                _context.PondsTbls.Add(pond);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool UpdatePond(PondsTbl pond)
        {
            var updatePond = GetPondById(pond.PondId);
            if (updatePond != null)
            {
                updatePond.Name = pond.Name;
                //updatePond.Image = pond.Image;
                updatePond.Depth = pond.Depth;
                updatePond.Volume = pond.Volume;
                updatePond.DrainCount = pond.DrainCount;
                updatePond.PumpCapacity = pond.PumpCapacity;
                _context.PondsTbls.Update(updatePond);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePond(int id)
        {
            var deletePond = GetPondById(id);
            if (deletePond != null)
            {
                _context.PondsTbls.Remove(deletePond);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
