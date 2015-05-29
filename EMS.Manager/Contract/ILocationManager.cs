using EMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Manager.Contract
{
    public interface ILocationManager
    {

        #region Province
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<ProvinceDTO> FindProvinces(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<ProvinceDTO> GetAllProvincesAreas(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<ProvinceDTO> GetAllLocationDetails(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProvinceDTO FindProvinceById(int id);

        /// <summary>
        /// To Delete a Profile
        /// </summary>
        /// <param name="profileId"></param>
        void DeleteProvince(int profileId);

         /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        void SaveProvinceInformation(ProvinceDTO profileDTO, string ip = "30.40.50.60");

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        void UpdateProvinceInformation(int id, ProvinceDTO profileDTO, string ip = "30.40.50.60");
#endregion

        #region AreaOffice
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<AreaDTO> FindAreaOffices(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AreaDTO FindAreaOfficeById(int id);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<AreaDTO> FindAreaOfficeByProvinceId(int provinceId);

        /// <summary>
        /// To Delete a Profile
        /// </summary>
        /// <param name="profileId"></param>
        void DeleteAreaOffice(int profileId);

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        void SaveAreaOfficeInformation(AreaDTO profileDTO,string ip="30.40.50.60");

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        void UpdateAreaOfficeInformation(int id, AreaDTO profileDTO, string ip = "30.40.50.60");
        #endregion

        #region PrimarySubstation
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<SubStationDTO> FindPrimarySubstations(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SubStationDTO FindPrimarySubstationById(int id);

        /// <summary>
        /// To Delete a Profile
        /// </summary>
        /// <param name="profileId"></param>
        void DeletePrimarySubstation(int profileId);

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        void SavePrimarySubstationInformation(SubStationDTO profileDTO, string ip = "30.40.50.60");

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        void UpdatePrimarySubstationInformation(int id, SubStationDTO profileDTO, string ip = "30.40.50.60");
        #endregion

        #region Supplier
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<SupplierDTO> FindSuppliers(int pageIndex, int pageCount, bool basicInfo = false);


        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        SupplierDTO FindSupplierById(int supplierId);

        /// <summary>
        /// To Delete a Profile
        /// </summary>
        /// <param name="supplierId"></param>
        void DeleteSupplier(int supplierId);

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="supplierDTO"></param>
        /// <returns></returns>
        void SaveSupplierInformation(SupplierDTO supplierDTO, string ip = "");

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierDTO"></param>
        void UpdateSupplierInformation(int supplierId, SupplierDTO supplierDTO, string ip = "");
        #endregion

        #region Meter
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<MeterDTO> FindMeters(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        MeterDTO FindMeterById(int supplierId);

        /// <summary>
        /// To Delete a Profile
        /// </summary>
        /// <param name="supplierId"></param>
        void DeleteMeter(int supplierId);

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="supplierDTO"></param>
        /// <returns></returns>
        void SaveMeterInformation(MeterDTO supplierDTO, string ip = "");

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierDTO"></param>
        void UpdateMeterInformation(int supplierId, MeterDTO supplierDTO, string ip = "");
        #endregion

        #region Meter Reading
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<MeterReadingDTO> FindMeterReadings(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        MeterReadingDTO FindMeterReadingById(int reportId);


        #endregion  Meter Reading

        #region Report
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<ReportDTO> FindReports(int pageIndex, int pageCount, bool basicInfo = false);

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        ReportDTO FindReportById(int reportId);


        #endregion  Meter Reading



    }
}
