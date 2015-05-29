
using EMS.Core.Entities;
using EMS.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Manager.Conversion
{
    public static class Mapping
    {

        #region Province 
        public static ProvinceDTO ProvinceToProvinceDTO(Province obj)
        {
            ProvinceDTO objProfileDTO = new ProvinceDTO();
            objProfileDTO.Id = obj.Id; ;
            objProfileDTO.Name = obj.Name;
            List<AreaDTO> lstAreaDTO = new List<AreaDTO>();
            foreach (var area in obj.Areas)
            {
                lstAreaDTO.Add(AreaToAreaDTO(area));
            }
            objProfileDTO.AreaDTO = lstAreaDTO;

            List<SubStationDTO> lstSubStationDTO = new List<SubStationDTO>();
            foreach (var subs in obj.SubStations)
            {
                lstSubStationDTO.Add(SubStationToSubStationDTO(subs));
            }
            objProfileDTO.SubStationDTO = lstSubStationDTO;

            return objProfileDTO;
        }

        public static ProvinceDTO BasicProvinceToProvinceDTO(Province obj)
        {
            ProvinceDTO objProfileDTO = new ProvinceDTO();
            objProfileDTO.Id = obj.Id; ;
            objProfileDTO.Name = obj.Name;
            return objProfileDTO;
        }


        public static ProvinceDTO BasicProvinceAreaToProvinceDTO(Province obj)
        {
            ProvinceDTO objProfileDTO = new ProvinceDTO();
            objProfileDTO.Id = obj.Id; ;
            objProfileDTO.Name = obj.Name;
            List<AreaDTO> lstAreaDTO = new List<AreaDTO>();
            foreach (var area in obj.Areas)
            {
                lstAreaDTO.Add(BasicAreaToAreaDTO(area));
            }
            objProfileDTO.AreaDTO = lstAreaDTO;
            return objProfileDTO;
        }

        #endregion

        #region Area

        public static AreaDTO AreaToAreaDTO(Area obj)
        {
            AreaDTO objDTO = new AreaDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.ProvinceId = obj.ProvinceId;

            List<SubStationDTO> lstSubStationDTO = new List<SubStationDTO>();
            foreach (var subs in obj.SubStations)
            {
                lstSubStationDTO.Add(SubStationToSubStationDTO(subs));
            }
            objDTO.SubStationDTO = lstSubStationDTO;



            return objDTO;
        }

        public static AreaDTO BasicAreaToAreaDTO(Area obj)
        {
            AreaDTO objDTO = new AreaDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.ProvinceId = obj.ProvinceId;
            return objDTO;
        }

        public static AreaDTO BasicAreaSubToAreaDTO(Area obj)
        {
            AreaDTO objDTO = new AreaDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.ProvinceId = obj.ProvinceId;

            List<SubStationDTO> lstSubStationDTO = new List<SubStationDTO>();
            foreach (var subs in obj.SubStations)
            {
                lstSubStationDTO.Add(SubStationToSubStationDTO(subs));
            }
            objDTO.SubStationDTO = lstSubStationDTO;

            return objDTO;
        }
        #endregion

        #region SubStation
        public static SubStationDTO SubStationToSubStationDTO(SubStation obj)
        {
            SubStationDTO objDTO = new SubStationDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.ProvinceId = obj.ProvinceId;
            objDTO.AreaId = obj.AreaId;

            return objDTO;
        }

        public static SubStationDTO BasicSubStaionToSubStationDTO(SubStation obj)
        {
            SubStationDTO objDTO = new SubStationDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.AreaId = obj.AreaId;
            objDTO.ProvinceId = obj.ProvinceId;
            return objDTO;
        }

        #endregion

        #region Supplier
        public static SupplierDTO SupplierToSupplierDTO(Supplier obj)
        {
            SupplierDTO objDTO = new SupplierDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;

            List<MeterDTO> lstMetersDTO = new List<MeterDTO>();
            foreach (var meter in obj.Meters)
            {
                lstMetersDTO.Add(MeterToMeterDTO(meter));
            }
            objDTO.MeterDTO = lstMetersDTO;

            return objDTO;
        }

        public static SupplierDTO BasicSupplierToSupplierDTO(Supplier obj)
        {
            SupplierDTO objDTO = new SupplierDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            return objDTO;
        }


        #endregion

        #region Meter
        public static MeterDTO MeterToMeterDTO(Meter obj)
        {
            MeterDTO objDTO = new MeterDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.Serial = obj.Serial;
            objDTO.FeederPoint = obj.FeederPoint;
            objDTO.SubStationId = obj.SubStationId;
            //Supplier should be customer ID
            //Also this can be ==0
            objDTO.SupplierId = obj.SupplierId;

            //Meter Reading Will not be processed here...

            return objDTO;
        }

        public static MeterDTO BasicMeterToMeterDTO(Meter obj)
        {
            MeterDTO objDTO = new MeterDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.Serial = obj.Serial; ;
            objDTO.FeederPoint = obj.FeederPoint;
            return objDTO;
        }

        #endregion

        #region Location
        public static ProvinceDTO BasicLocationDTO(Province obj)
        {
            ProvinceDTO objProfileDTO = new ProvinceDTO();
            objProfileDTO.Id = obj.Id; ;
            objProfileDTO.Name = obj.Name;
            List<AreaDTO> lstAreaDTO = new List<AreaDTO>();
            foreach (var area in obj.Areas)
            {
                lstAreaDTO.Add(BasicAreaSubToAreaDTO(area));
            }
            objProfileDTO.AreaDTO = lstAreaDTO;
            return objProfileDTO;
        }
        #endregion

        #region Report

        // Report mapping --> thilini
        public static ReportDTO ReportToReportDTO(Report obj)
        {
            ReportDTO objDTO = new ReportDTO();
            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.ReportDate = obj.ReportDate;
            objDTO.SupplierId = obj.SupplierId;

            List<MeterReadingDTO> lstMeterReadingDTO = new List<MeterReadingDTO>();
            foreach (var meterReading in obj.MeterReadings)
            {
                lstMeterReadingDTO.Add(MeterReadingToMeterReading(meterReading));
            }
            objDTO.MeterReadingDTO = lstMeterReadingDTO;

            return objDTO;
        }
        //Report mapping ends
        //----> thilini ends


        public static ReportDTO BasicReportToReportDTO(Report obj)
        {
            ReportDTO objDTO = new ReportDTO();

            objDTO.Id = obj.Id; ;
            objDTO.Name = obj.Name;
            objDTO.ReportDate = obj.ReportDate;
            objDTO.SupplierId = obj.SupplierId;

            return objDTO;
        }


        #endregion

        #region MeterReading
        public static MeterReadingDTO BasicMeterReadingToMeterReadingDTO(MeterReading obj)
        {
            MeterReadingDTO objDTO = new MeterReadingDTO();
            objDTO.Id = obj.Id; ;
            objDTO.ReadingDate = obj.ReadingDate;
            objDTO.Remarks = obj.Remarks;
            objDTO.DayValue = obj.DayValue;
            objDTO.PeakValue = obj.PeakValue;
            objDTO.OffPeakValue = obj.OffPeakValue;

            objDTO.CoincidentPeak = obj.CoincidentPeak;
            objDTO.MeterId = obj.MeterId;
            objDTO.ReportId = obj.ReportId;


            return objDTO;
        }

        // thilini start
        // Meter reading mapping
        public static MeterReadingDTO MeterReadingToMeterReading(MeterReading obj)
        {
            MeterReadingDTO objDTO = new MeterReadingDTO();
            objDTO.Id = obj.Id; ;
            objDTO.ReadingDate = obj.ReadingDate;
            objDTO.Remarks = obj.Remarks;
            objDTO.DayValue = obj.DayValue;
            objDTO.PeakValue = obj.PeakValue;
            objDTO.OffPeakValue = obj.OffPeakValue;

            objDTO.CoincidentPeak = obj.CoincidentPeak;
            objDTO.MeterId = obj.MeterId;
            objDTO.ReportId = obj.ReportId;



            return objDTO;
        }
        //end
        #endregion

    }
}
