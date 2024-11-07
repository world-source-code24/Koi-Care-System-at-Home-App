using Business_Object.Models;
using KoiCare_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_Services
{
    public class WaterParaService : IWaterParaService
    {
        private readonly IWaterParaRepo _waterParaRepo;

        public WaterParaService()
        {
            _waterParaRepo = new WaterParaRepo();
        }
        public bool CreateWaterParameter(WaterParametersTbl waterPara)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWaterParameter(int id)
        {
            throw new NotImplementedException();
        }

        public WaterParametersTbl GetWaterDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public List<WaterParametersTbl> GetWaterParaByAccId(int accId)
        {
            return _waterParaRepo.GetWaterParaByAccId(accId);
        }

        public List<WaterParametersTbl> GetWaterParameter()
        {
            // Tạo danh sách dữ liệu giả lập
            List<WaterParametersTbl> waterParameters = new List<WaterParametersTbl>
            {
                new WaterParametersTbl
                {
                    ParameterId = 1,
                    Temperature = 25.5m,
                    Salt = 0.5m,
                    PhLevel = 7.0m,
                    O2Level = 8.0m,
                    No2Level = 0.1m,
                    No3Level = 0.5m,
                    Po4Level = 0.05m,
                    TotalChlorines = 0.1m,
                    Date = DateTime.Now,
                    Note = "Water is clear.",
                    PondId = 1
                },
                new WaterParametersTbl
                {
                    ParameterId = 2,
                    Temperature = 26.0m,
                    Salt = 0.4m,
                    PhLevel = 6.8m,
                    O2Level = 7.5m,
                    No2Level = 0.2m,
                    No3Level = 0.4m,
                    Po4Level = 0.04m,
                    TotalChlorines = 0.2m,
                    Date = DateTime.Now,
                    Note = "Some algae present.",
                    PondId = 1
                },
                new WaterParametersTbl
                {
                    ParameterId = 1,
                    Temperature = 25.5m,
                    Salt = 0.5m,
                    PhLevel = 7.0m,
                    O2Level = 8.0m,
                    No2Level = 0.1m,
                    No3Level = 0.5m,
                    Po4Level = 0.05m,
                    TotalChlorines = 0.1m,
                    Date = DateTime.Now,
                    Note = "Water is clear.",
                    PondId = 1
                },
                new WaterParametersTbl
                {
                    ParameterId = 1,
                    Temperature = 25.5m,
                    Salt = 0.5m,
                    PhLevel = 7.0m,
                    O2Level = 8.0m,
                    No2Level = 0.1m,
                    No3Level = 0.5m,
                    Po4Level = 0.05m,
                    TotalChlorines = 0.1m,
                    Date = DateTime.Now,
                    Note = "Water is clear.",
                    PondId = 1
                },
                new WaterParametersTbl
                {
                    ParameterId = 1,
                    Temperature = 25.5m,
                    Salt = 0.5m,
                    PhLevel = 7.0m,
                    O2Level = 8.0m,
                    No2Level = 0.1m,
                    No3Level = 0.5m,
                    Po4Level = 0.05m,
                    TotalChlorines = 0.1m,
                    Date = DateTime.Now,
                    Note = "Water is clear.",
                    PondId = 1
                },
                new WaterParametersTbl
                {
                    ParameterId = 1,
                    Temperature = 25.5m,
                    Salt = 0.5m,
                    PhLevel = 7.0m,
                    O2Level = 8.0m,
                    No2Level = 0.1m,
                    No3Level = 0.5m,
                    Po4Level = 0.05m,
                    TotalChlorines = 0.1m,
                    Date = DateTime.Now,
                    Note = "Water is clear.",
                    PondId = 1
                },
                // Thêm các bản ghi khác nếu cần
            };
            return waterParameters;
        }

        public bool UpdateWaterParameter(WaterParametersTbl waterPara)
        {
            throw new NotImplementedException();
        }
    }
}
