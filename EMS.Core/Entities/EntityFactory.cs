using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Core.Entities
{
    /// <summary>
    /// This is the factory for Profile creation
    /// </summary>
    public static class EntityFactory
    {

        public static Province CreateProvince(string name, string ip="10.20.30.40")
        {
            Province obj = new Province();

            obj.Name= name;



            obj.CreatedDate=DateTime.Now;
            obj.AlteredDate = DateTime.Now;
            obj.IP = ip;
            return obj;
        }

        public static Area CreateArea(string name, int provinceId, string ip = "10.20.30.40")
        {
            Area obj = new Area();

            obj.Name = name;
            obj.ProvinceId = provinceId;

            obj.CreatedDate = DateTime.Now;
            obj.AlteredDate = DateTime.Now;
            obj.IP = ip;
            return obj;
        }

        public static SubStation CreateSubStation(string name,int areaId, int provinceId=0, string ip = "10.20.30.40")
        {
            SubStation obj = new SubStation();

            obj.Name = name;
            obj.AreaId = areaId;

            obj.ProvinceId= provinceId;
            

            obj.CreatedDate = DateTime.Now;
            obj.AlteredDate = DateTime.Now;
            obj.IP = ip;
            return obj;
        }

        public static Supplier CreateSupplier(string name,  string ip = "10.20.30.40")
        {
            Supplier obj = new Supplier();

            obj.Name = name;

            obj.CreatedDate = DateTime.Now;
            obj.AlteredDate = DateTime.Now;
            obj.IP = ip;
            return obj;
        }

        public static Meter CreateMeter(string serial, string name, int substationId, int supplierId, string feedingpoint = "", string ip = "10.20.30.40")
        {
            Meter obj = new Meter();

            obj.Name = name;
            obj.Serial=serial;
            obj.FeederPoint=feedingpoint;
            obj.SubStationId = substationId;
            obj.SupplierId = supplierId;

            obj.CreatedDate = DateTime.Now;
            obj.AlteredDate = DateTime.Now;
            obj.IP = ip;
            return obj;
        }


        public static MeterReading CreateMeterReading(
        int meterId,
        DateTime readingDate,
        decimal dayValue,
        decimal peakValue,
        decimal offPeakValue,
        decimal coincidentPeakValue,
        string remarks="",
        string ip = "10.20.30.40")
        {
            MeterReading obj = new MeterReading();

            obj.MeterId=meterId;
            obj.PeakValue = peakValue;
            obj.OffPeakValue = offPeakValue;
            obj.CoincidentPeak = coincidentPeakValue;
            obj.Remarks = remarks;
            obj.ReadingDate=readingDate;



            obj.CreatedDate = DateTime.Now;
            obj.AlteredDate = DateTime.Now;
            obj.IP = ip;
            return obj;
        }
     }



}
