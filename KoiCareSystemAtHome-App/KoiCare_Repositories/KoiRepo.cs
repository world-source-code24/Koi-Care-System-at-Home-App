using Business_Object.Models;
using KoiCare_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Repositories
{
    public class KoiRepo : IKoiRepo
    {
        public bool CreateKoi(KoisTbl koi)
            =>KoiDAO.Instance.CreateKoi(koi);
        public bool DeleteKoi(int id)
            =>KoiDAO.Instance.DeleteKoi(id);

        public  List<KoisTbl> GetAllKoiByPondId(int pondId)
        =>  KoiDAO.Instance.GetAllKoiByPondId(pondId);

        public  KoisTbl GetKoiById(int id)
        => KoiDAO.Instance.GetKoiById(id);

        public  List<KoisTbl> GetKois()
            => KoiDAO.Instance.GetKois();
        public bool UpdateKoi(KoisTbl koi)
            => KoiDAO.Instance.UpdateKoi(koi);
    }
}
