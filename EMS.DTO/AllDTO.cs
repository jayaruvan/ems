using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DTO
{
    public class ProvinceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<AreaDTO> AreaDTO { get; set; }
        public List<SubStationDTO> SubStationDTO { get; set; }

        //public int AreasCount=AreasDTO.Count();
        //public int SubStationCount = AreaDTO.Count();
    
    }


    public class AreaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProvinceId { get; set; }
        public List<SubStationDTO> SubStationDTO { get; set; }
    }

    public class SubStationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProvinceId { get; set; }
        public int AreaId { get; set; }

        public List<MeterDTO> MeterDTO { get; set; }
    }

    public class MeterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public string FeederPoint { get; set; }


        public int SupplierId { get; set; }
        public int SubStationId { get; set; }

        public List<MeterReadingDTO> MeterReadingDTO { get; set; }
    }

    public class MeterReadingDTO
    {
        public int Id { get; set; }
        public DateTime ReadingDate { get; set; }
        public string Remarks { get; set; }

        public decimal DayValue { get; set; }
        public decimal PeakValue { get; set; }
        public decimal OffPeakValue { get; set; }
        public decimal CoincidentPeak { get; set; }


        public int MeterId { get; set; }
        public int ReportId { get; set; }

    }

    public class ReportDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public System.DateTime ReportDate { get; set; }
        public int SupplierId { get; set; }

        public List<MeterReadingDTO> MeterReadingDTO { get; set; }
    }

    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MeterDTO> MeterDTO { get; set; }
        public List<ReportDTO> ReportDTO { get; set; }

    }


}
