using Business_Object.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class KoiDAO
    {
        private KoicareathomeContext _context;
        private static KoiDAO instance = null;

        public KoiDAO()
        {
            _context = new KoicareathomeContext();
        }

        public static KoiDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KoiDAO();
                }
                return instance;
            }
        }
        public List<KoisTbl> GetKois(int accId)
        {
            return _context.KoisTbls.Include(p => p.Pond).Where(p => p.Pond.AccId == accId).ToList();
        }

        public List<KoisTbl> GetAllKoiByPondId(int pondId)
        {
            return _context.KoisTbls.Where(k => k.PondId == pondId).ToList();
        }

        public KoisTbl GetKoiById(int id)
        {
            return _context.KoisTbls.SingleOrDefault(k => k.KoiId.Equals(id));
        } 

        public bool CreateKoi(KoisTbl koi)
        {
            bool isSuccess = true;
            try
            {
                _context.KoisTbls.Add(koi);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool UpdateKoi(KoisTbl koi)
        {
            var updateKoi = GetKoiById(koi.KoiId);
            if (updateKoi != null)
            {
                updateKoi.Name = koi.Name;
                updateKoi.Age = koi.Age;
                updateKoi.Breed = koi.Breed;
                updateKoi.Length = koi.Length;
                updateKoi.Weight = koi.Weight;
                updateKoi.Sex = koi.Sex;
                updateKoi.Physique = koi.Physique;
                updateKoi.PondId = koi.PondId;
                _context.KoisTbls.Update(updateKoi);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteKoi(int id)
        {
            var deleteKoi = GetKoiById(id);
            if (deleteKoi != null)
            {
                _context.KoisTbls.Remove(deleteKoi);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }

}
