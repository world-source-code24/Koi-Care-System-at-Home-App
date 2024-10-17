using Business_Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public interface IKoiRepo
    {
        public KoisTbl GetKoiById(int id);
        public List<KoisTbl> GetAllKoiByPondId(int pondId);
        public List<KoisTbl> GetKois();
        public bool CreateKoi(KoisTbl koi);
        public bool UpdateKoi(KoisTbl koi);
        public bool DeleteKoi(int id);

    }
}
