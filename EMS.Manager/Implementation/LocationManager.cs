using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMS.Manager.Conversion;
using EMS.Common.Validator;

using EMS.Common;
using EMS.DTO;
using EMS.Manager.Contract;
using EMS.Common.Logging;
using EMS.Manager.Resources;
using EMS.Core.Entities;
using EMS.Repository;
using EMS.DAL;

namespace EMS.Manager.Implementation
{
    public class LocationManager : ILocationManager
    {
        #region Global Declearation

        ProvinceRepository _provinceRepository;
        AreaRepository     _areaOfficeRepository;
        SubStationRepository _primarySubsationRepository;
        MeterRepository _meterRepository;
        SupplierRepository _supplierRepository;

        MeterReadingRepository _meterReadingRepository;
        ReportRepository _reportRepository;



        #endregion Global Declearation

        #region Constructor

        public LocationManager(
            ProvinceRepository provinceRepository,
            AreaRepository areaOfficeRepository,
            SubStationRepository primarySubstationRepository,
            MeterRepository meterRepository,
            SupplierRepository supplierRepository,
            MeterReadingRepository meterReadingRepository,
            ReportRepository reportRepository

            )
        {
            if (provinceRepository == null)
                throw new ArgumentNullException("provinceRepository");

            if (areaOfficeRepository == null)
                throw new ArgumentNullException("areaOfficeRepository");

            if (primarySubstationRepository == null)
                throw new ArgumentNullException("primarySubstationRepository");

            if (meterRepository == null)
                throw new ArgumentNullException("MeterRepository NULL");

            if (supplierRepository == null)
                throw new ArgumentNullException("SupplierRepository NULL");

            if (meterReadingRepository == null)
                throw new ArgumentNullException("MeterReadingRepository NULL");

            if (reportRepository == null)
                throw new ArgumentNullException("ReportRepository NULL");

            _provinceRepository = provinceRepository;
            _areaOfficeRepository = areaOfficeRepository;
            _primarySubsationRepository = primarySubstationRepository;

            _meterRepository = meterRepository;
            _supplierRepository = supplierRepository;

            _meterReadingRepository = meterReadingRepository;
            _reportRepository = reportRepository;

        }

        #endregion Constructor


        #region Interface Implementation

        #region Province
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<ProvinceDTO> FindProvinces(int pageIndex, int pageCount,bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles =_provinceRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);
            var areas = _areaOfficeRepository.GetAll().ToList();
            //var subStations = _primarySubsationRepository.GetAll().ToList(); ;

            if (profiles != null
                &&
                profiles.Any())
            {
                List<ProvinceDTO> lstProfileDTO = new List<ProvinceDTO>();
                foreach (var profile in profiles)
                {
                    if (basicInfo)
                    {
                        lstProfileDTO.Add(Conversion.Mapping.BasicProvinceToProvinceDTO(profile));

                    }
                    else
                    {
                        lstProfileDTO.Add(Conversion.Mapping.ProvinceToProvinceDTO(profile));
                    }
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<ProvinceDTO>();
        }


        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<ProvinceDTO> GetAllProvincesAreas(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _provinceRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);
            var areas = _areaOfficeRepository.GetAll().ToList();
            //var subStations = _primarySubsationRepository.GetAll().ToList(); ;

            if (profiles != null
                &&
                profiles.Any())
            {
                List<ProvinceDTO> lstProfileDTO = new List<ProvinceDTO>();
                foreach (var profile in profiles)
                {
                    if (basicInfo)
                    {
                        lstProfileDTO.Add(Conversion.Mapping.BasicProvinceAreaToProvinceDTO(profile));
                    }
                    else
                    {
                        lstProfileDTO.Add(Conversion.Mapping.ProvinceToProvinceDTO(profile));
                    }
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<ProvinceDTO>();
        }


        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<ProvinceDTO> GetAllLocationDetails(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _provinceRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);
            var areas = _areaOfficeRepository.GetAll().ToList();
            var substations = _primarySubsationRepository.GetAll().ToList();

            //var subStations = _primarySubsationRepository.GetAll().ToList(); ;

            if (profiles != null
                &&
                profiles.Any())
            {
                List<ProvinceDTO> lstProfileDTO = new List<ProvinceDTO>();
                foreach (var profile in profiles)
                {

                        lstProfileDTO.Add(Conversion.Mapping.BasicLocationDTO(profile));

                }
                return lstProfileDTO;
            }
            else // no data
                return new List<ProvinceDTO>();
        }

        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<ProvinceDTO> GetAllProvinceNames(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _provinceRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);

            if (profiles != null
                &&
                profiles.Any())
            {
                List<ProvinceDTO> lstProfileDTO = new List<ProvinceDTO>();
                foreach (var profile in profiles)
                {
                    lstProfileDTO.Add(Conversion.Mapping.BasicProvinceToProvinceDTO(profile));
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<ProvinceDTO>();
        }


        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteProvince(int profileId)
        {
            var profile = _provinceRepository.Get(profileId);

            if (profile != null) //if profile exist
            {
                //Delete all addresses associate with this profile 
                /**
                List<ProfileAddress> lstProfileAddress = profile.ProfileAddresses.ToList();
                foreach (ProfileAddress objProfileAddress in lstProfileAddress)
                {
                    this.DeleteProfileAddress(objProfileAddress);
                }
                
                //Delete all phones associate with this profile 
                List<ProfilePhone> lstProfilePhone = profile.ProfilePhones.ToList();
                foreach (ProfilePhone objProfilePhone in lstProfilePhone)
                {
                    this.DeleteProfilePhone(objProfilePhone);
                }
                **/
                _provinceRepository.Remove(profile);

                //commit changes
                _provinceRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
        }

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProvinceDTO FindProvinceById(int id)
        {

            //recover orders
            var profile = _provinceRepository.Get(id);


            if (profile != null)
            {
                return Conversion.Mapping.ProvinceToProvinceDTO(profile);
            }
            else //no data
                return new ProvinceDTO();

        }
        /**
        /// <summary>
        /// Get all initialization data for Contact page
        /// </summary>
        /// <returns></returns>
        public ContactForm InitializePageData()
        {
            var addressTypes = _addressTypeRepository.GetAll().ToList();
            var phoneTypes = _phoneTypeRepository.GetAll().ToList();

            ContactForm objContactForm = new ContactForm();
            objContactForm.lstAddressTypeDTO = Conversion.Mapping.AddressTypeToAddressTypeDTO(addressTypes);
            objContactForm.lstPhoneTypeDTO = Conversion.Mapping.PhoneTypeToPhoneTypeDTO(phoneTypes);

            return objContactForm;
        }
         * **/

        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        public void SaveProvinceInformation(ProvinceDTO profileDTO, string ip = "30.40.50.60")
        {
            //if profileDTO data is not valid
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var newProfile =EntityFactory.CreateProvince(profileDTO.Name,ip);

            //Save Profile
            newProfile = SaveProvince(newProfile);
            /**
            //if profileDTO contains any address
            if (profileDTO.AddressDTO != null)
            {
                foreach (AddressDTO objAddressDTO in profileDTO.AddressDTO)
                {
                    this.SaveAddress(objAddressDTO, newProfile);
                }
            }

            //if profileDTO contains any phone
            if (profileDTO.PhoneDTO != null)
            {
                foreach (PhoneDTO objPhoneDTO in profileDTO.PhoneDTO)
                {
                    this.SavePhone(objPhoneDTO, newProfile);
                }
            }
             * **/
        }

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        public void UpdateProvinceInformation(int id, ProvinceDTO profileDTO, string ip = "30.40.50.60")
        {
            //if profileDTO data is not valid
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var currentProfile = _provinceRepository.Get(id);

            //Assign updated value to existing profile
            var updatedProfile = new Province ();
            updatedProfile.Id= id;
            updatedProfile.Name= profileDTO.Name;

            //Update Profile
            updatedProfile = this.UpdateProvince(currentProfile, updatedProfile);
            /**
            //Update Address
            List<AddressDTO> lstUpdatedAddressDTO = profileDTO.AddressDTO;
            List<ProfileAddress> lstCurrentAddress = _profileAddressRepository.GetFiltered(x => x.ProfileId.Equals(id)).ToList();

            UpdateAddress (lstUpdatedAddressDTO, lstCurrentAddress, updatedProfile);

            //Update Phone
            List<PhoneDTO> lstUpdatedPhoneDTO = profileDTO.PhoneDTO;
            List<ProfilePhone> lstCurrentPhone = _profilePhoneRepository.GetFiltered(x => x.ProfileId.Equals(id)).ToList();

            UpdatePhone(lstUpdatedPhoneDTO, lstCurrentPhone, updatedProfile);
             * **/
        }

        #endregion

        #region Area
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<AreaDTO> FindAreaOffices(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _areaOfficeRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);
            var substations = _primarySubsationRepository.GetAll().ToList();
            if (profiles != null
                &&
                profiles.Any())
            {

                // Subs in Area Offices ??
                List<AreaDTO> lstProfileDTO = new List<AreaDTO>();
                foreach (var profile in profiles)
                {
                    lstProfileDTO.Add(Conversion.Mapping.AreaToAreaDTO(profile));
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<AreaDTO>();
        }

        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<AreaDTO> FindAreaOfficeByProvinceId(int provinceId)
        {
            if (provinceId < 0 )
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var areas  = _areaOfficeRepository.GetFiltered(a=>a.ProvinceId==provinceId);
                
                
                //_provinceRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);

            if (areas != null
                &&
                areas.Any())
            {
                List<AreaDTO> lstAreasDTO = new List<AreaDTO>();
                foreach (var area in areas)
                {
                    lstAreasDTO.Add(Conversion.Mapping.BasicAreaToAreaDTO(area));
                }
                return lstAreasDTO;
            }
            else // no data
                return new List<AreaDTO>();
        }

        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteAreaOffice(int areaOfficeId)
        {
            var profile = _areaOfficeRepository.Get(areaOfficeId);

            //** If child nodes exist you cant delete parent node **/

            if (profile != null) //if profile exist
            {

                //Delete all addresses associate with this profile 
                /**
                List<ProfileAddress> lstProfileAddress = profile.ProfileAddresses.ToList();
                foreach (ProfileAddress objProfileAddress in lstProfileAddress)
                {
                    this.DeleteProfileAddress(objProfileAddress);
                }
                
                //Delete all phones associate with this profile 
                List<ProfilePhone> lstProfilePhone = profile.ProfilePhones.ToList();
                foreach (ProfilePhone objProfilePhone in lstProfilePhone)
                {
                    this.DeleteProfilePhone(objProfilePhone);
                }
                **/
                _areaOfficeRepository.Remove(profile);

                //commit changes
                _areaOfficeRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning("Messages.warning_Can not Remove Non Existing Area ");
        }

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AreaDTO FindAreaOfficeById(int id)
        {

            //recover orders
            var profile = _areaOfficeRepository.Get(id);


            if (profile != null)
            {
                return Conversion.Mapping.AreaToAreaDTO(profile);
            }
            else //no data
                return new AreaDTO();

        }


        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        public void SaveAreaOfficeInformation(AreaDTO profileDTO,string ip="20.30.40.50")
        {
            //if profileDTO data is not valid
            if (profileDTO == null)
                throw new ArgumentException("Messages.warning_CannotAddProfileWithNullInformation");

            //Create a new profile entity
            var newProfile = EntityFactory.CreateArea(profileDTO.Name,profileDTO.ProvinceId,ip);

            //Save Profile
            newProfile = SaveAreaOffice(newProfile);

        }

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        public void UpdateAreaOfficeInformation(int id, AreaDTO profileDTO,string ip="20.30.40.50")
        {
            //if profileDTO data is not valid
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var currentProfile = _areaOfficeRepository.Get(id);

            //Assign updated value to existing profile
            var updatedProfile = new Area ();
            updatedProfile.Id = id;
            updatedProfile.IP = ip;
            updatedProfile.Name = profileDTO.Name;

            //Update Profile
            updatedProfile = this.UpdateAreaOffice(currentProfile, updatedProfile);

        }

        #endregion
        
        #region SubStation
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<SubStationDTO> FindPrimarySubstations(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _primarySubsationRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);

            if (profiles != null
                &&
                profiles.Any())
            {
                List<SubStationDTO> lstProfileDTO = new List<SubStationDTO>();
                foreach (var profile in profiles)
                {
                    lstProfileDTO.Add(Conversion.Mapping.SubStationToSubStationDTO(profile));
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<SubStationDTO>();
        }

        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeletePrimarySubstation(int areaOfficeId)
        {
            var profile = _primarySubsationRepository.Get(areaOfficeId);

            //** If child nodes exist you cant delete parent node **/

            if (profile != null) //if profile exist
            {


                _primarySubsationRepository.Remove(profile);

                //commit changes
                _primarySubsationRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning("Messages.warning_Can not Remove Non Existing Area ");
        }

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubStationDTO FindPrimarySubstationById(int id)
        {

            //recover orders
            var profile = _primarySubsationRepository.Get(id);


            if (profile != null)
            {
                return Conversion.Mapping.SubStationToSubStationDTO(profile);
            }
            else //no data
                return new SubStationDTO();

        }


        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        public void SavePrimarySubstationInformation(SubStationDTO objDTO, string ip = "30.40.50.60")
        {
            //if profileDTO data is not valid
            if (objDTO == null)
                throw new ArgumentException("Messages.warning_CannotAddProfileWithNullInformation");

            //Create a new profile entity
            var newProfile = EntityFactory.CreateSubStation(objDTO.Name,objDTO.AreaId,objDTO.ProvinceId,"20.30.40.50");

            //Save Profile
            newProfile = SavePrimarySubstation(newProfile);

        }

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        public void UpdatePrimarySubstationInformation(int id, SubStationDTO profileDTO, string ip = "30.40.50.60")
        {
            //if profileDTO data is not valid
            if (profileDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var currentProfile = _primarySubsationRepository.Get(id);

            //Assign updated value to existing profile
            var updatedProfile = new SubStation();
            updatedProfile.Id= id;
            updatedProfile.Name = profileDTO.Name;
            updatedProfile.AreaId= profileDTO.AreaId;
            updatedProfile.ProvinceId= profileDTO.ProvinceId;

            //Update Profile
            updatedProfile = this.UpdatePrimarySubstation(currentProfile, updatedProfile);

        }

        #endregion

        #region Supplier
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<SupplierDTO> FindSuppliers(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var suppliers = _supplierRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);
            var meters = _meterRepository.GetAll().ToList();

            if (suppliers != null
                &&
                suppliers.Any())
            {
                List<SupplierDTO> lstSupplierDTO = new List<SupplierDTO>();
                foreach (var supplier in suppliers)
                {


                    if (basicInfo)
                    {

                        lstSupplierDTO.Add(Conversion.Mapping.BasicSupplierToSupplierDTO(supplier));
                    }
                    else
                    {
                        lstSupplierDTO.Add(Conversion.Mapping.SupplierToSupplierDTO(supplier));
                    }
                }
                return lstSupplierDTO;
            }
            else // no data
                return new List<SupplierDTO>();
        }

        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<SupplierDTO> GetAllSupplierNames(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var suppliers = _supplierRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);



            if (suppliers != null
                &&
                suppliers.Any())
            {
                List<SupplierDTO> lstSupplierDTO = new List<SupplierDTO>();
                foreach (var supplier in suppliers)
                {
                    lstSupplierDTO.Add(Conversion.Mapping.SupplierToSupplierDTO(supplier));
                }
                return lstSupplierDTO;
            }
            else // no data
                return new List<SupplierDTO>();
        }
        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteSupplier(int profileId)
        {
            var profile = _supplierRepository.Get(profileId);

            if (profile != null) //if profile exist
            {
                _supplierRepository.Remove(profile);

                //commit changes
                _supplierRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
        }

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplierDTO FindSupplierById(int id)
        {

            //recover orders
            var profile = _supplierRepository.Get(id);


            if (profile != null)
            {
                return Conversion.Mapping.SupplierToSupplierDTO(profile);
            }
            else //no data
                return new SupplierDTO();

        }


        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="objDTO"></param>
        /// <returns></returns>
        public void SaveSupplierInformation(SupplierDTO objDTO, string ip = "")
        {
            //if objDTO data is not valid
            if (objDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var newObject = EntityFactory.CreateSupplier(objDTO.Name, ip);

            //Save Profile
            newObject = SaveSupplier(newObject);

        }

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objDTO"></param>
        public void UpdateSupplierInformation(int id, SupplierDTO objDTO, string ip = "")
        {
            //if objDTO data is not valid
            if (objDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var currentProfile = _supplierRepository.Get(id);

            //Assign updated value to existing profile
            var updatedObject = new Supplier();
            updatedObject.Id = id;
            updatedObject.Name = objDTO.Name;

            //Update Profile
            updatedObject = this.UpdateSupplier(currentProfile, updatedObject);

        }

        #endregion

        #region Meter
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MeterDTO> FindMeters(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _meterRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);


            if (profiles != null
                &&
                profiles.Any())
            {
                List<MeterDTO> lstProfileDTO = new List<MeterDTO>();
                foreach (var profile in profiles)
                {
                    if (basicInfo)
                    {

                        lstProfileDTO.Add(Conversion.Mapping.BasicMeterToMeterDTO(profile));
                    }
                    else
                    {
                        lstProfileDTO.Add(Conversion.Mapping.MeterToMeterDTO(profile));
                    }
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<MeterDTO>();
        }

        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteMeter(int profileId)
        {
            var profile = _meterRepository.Get(profileId);

            if (profile != null) //if profile exist
            {

                _meterRepository.Remove(profile);

                //commit changes
                _meterRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
        }

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MeterDTO FindMeterById(int id)
        {

            //recover orders
            var profile = _meterRepository.Get(id);


            if (profile != null)
            {
                return Conversion.Mapping.MeterToMeterDTO(profile);
            }
            else //no data
                return new MeterDTO();

        }


        /// <summary>
        /// Add new profile
        /// </summary>
        /// <param name="objDTO"></param>
        /// <returns></returns>
        public void SaveMeterInformation(MeterDTO objDTO, string ip = "")
        {
            //if objDTO data is not valid
            if (objDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var newObject = EntityFactory.CreateMeter(
                    objDTO.Serial,
                    objDTO.Name,
                    objDTO.SubStationId,
                    objDTO.SupplierId,
                    objDTO.FeederPoint,
                    ip);

            //Save Profile
            newObject = SaveMeter(newObject);

        }

        /// <summary>
        /// Update existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objDTO"></param>
        public void UpdateMeterInformation(int id, MeterDTO objDTO, string ip = "")
        {
            //if objDTO data is not valid
            if (objDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

            //Create a new profile entity
            var currentProfile = _meterRepository.Get(id);

            //Assign updated value to existing profile
            var updatedObject = new Meter();
            updatedObject.Id = id;
            updatedObject.Name = objDTO.Name;
            updatedObject.Serial = objDTO.Serial;
            updatedObject.FeederPoint = objDTO.FeederPoint;
            updatedObject.SubStationId = objDTO.SubStationId;
            updatedObject.SupplierId = objDTO.SupplierId;

            //Update Profile
            updatedObject = this.UpdateMeter(currentProfile, updatedObject);

        }

        #endregion

        #region Report
        /// <summary>
        /// Get all Profiles
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<ReportDTO> FindReports(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion
            var profiles = _reportRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);


            if (profiles != null
                &&
                profiles.Any())
            {
                List<ReportDTO> lstProfileDTO = new List<ReportDTO>();
                foreach (var profile in profiles)
                {
                    if (basicInfo)
                    {

                        lstProfileDTO.Add(Conversion.Mapping.BasicReportToReportDTO(profile));
                    }
                    else
                    {
                        lstProfileDTO.Add(Conversion.Mapping.ReportToReportDTO(profile));
                    }
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<ReportDTO>();
        }

        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="profileId"></param>
        public void DeleteReport(int profileId)
        {
            var profile = _reportRepository.Get(profileId);

            if (profile != null) //if profile exist
            {

                _reportRepository.Remove(profile);

                //commit changes
                _reportRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingProfile);
        }

        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReportDTO FindReportById(int id)
        {

            //recover orders
            var profile = _reportRepository.Get(id);


            if (profile != null)
            {
                return Conversion.Mapping.ReportToReportDTO(profile);
            }
            else //no data
                return new ReportDTO();

        }

        /*
                /// <summary>
                /// Add new profile
                /// </summary>
                /// <param name="objDTO"></param>
                /// <returns></returns>
                public void SaveReportInformation(ReportDTO objDTO, string ip = "")
                {
                    //if objDTO data is not valid
                    if (objDTO == null)
                        throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);
          
                    //*** create method in Entityfactory for create new report 

                    //Create a new profile entity
                    var newObject = EntityFactory.cre(
                            objDTO.
                            objDTO.Name,
                            objDTO.SupplierId,
                            objDTO.FeederPoint,
                            ip);

                    //Save Profile
                    newObject = SaveReport(newObject);

                }

                /// <summary>
                /// Update existing profile
                /// </summary>
                /// <param name="id"></param>
                /// <param name="objDTO"></param>
                public void UpdateReportInformation(int id, ReportDTO objDTO, string ip = "")
                {
                    //if objDTO data is not valid
                    if (objDTO == null)
                        throw new ArgumentException(Messages.warning_CannotAddProfileWithNullInformation);

                    //Create a new profile entity
                    var currentProfile = _reportRepository.Get(id);

                    //Assign updated value to existing profile
                    var updatedObject = new Report();
                    updatedObject.Id = id;
                    updatedObject.Name = objDTO.Name;

                    //Update Profile
                    updatedObject = this.UpdateReport(currentProfile, updatedObject);

                }*/

        #endregion Report

        #region Meter Reading
        /// <summary>
        /// Get all Meter Readings
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<MeterReadingDTO> FindMeterReadings(int pageIndex, int pageCount, bool basicInfo = false)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProfiles);

            //recover profiles in paged fashion

            var profiles = _meterReadingRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.CreatedDate, false);


            if (profiles != null
                &&
                profiles.Any())
            {
                List<MeterReadingDTO> lstProfileDTO = new List<MeterReadingDTO>();
                foreach (var profile in profiles)
                {
                    if (basicInfo)
                    {

                        lstProfileDTO.Add(Conversion.Mapping.BasicMeterReadingToMeterReadingDTO(profile));
                    }
                    else
                    {
                        lstProfileDTO.Add(Conversion.Mapping.MeterReadingToMeterReading(profile));
                    }
                }
                return lstProfileDTO;
            }
            else // no data
                return new List<MeterReadingDTO>();
        }



        /// <summary>
        /// Find Profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MeterReadingDTO FindMeterReadingById(int id)
        {

            //recover orders
            var profile = _meterReadingRepository.Get(id);


            if (profile != null)
            {
                return Conversion.Mapping.MeterReadingToMeterReading(profile);
            }
            else //no data
                return new MeterReadingDTO();

        }
        // something wrong in converting data types please check AllDTO

        #endregion Meter Reading
        
        #endregion Interface Implementation

        #region Private Methods




        #region province

        /// <summary>
        /// Save Profile
        /// </summary>
        /// <param name="profile"></param>
        Province SaveProvince(Province profile)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(profile))//if entity is valid save. 
            {
                //add profile and commit changes
                _provinceRepository.Add(profile);
                _provinceRepository.UnitOfWork.Commit();
                return profile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profile));
        }

        /// <summary>
        /// Update existing Profile
        /// </summary>
        /// <param name="profile"></param>
        Province UpdateProvince(Province currentProfile, Province updatedProfile, string ip="20.30.40.50")
        {
            updatedProfile.CreatedDate = currentProfile.CreatedDate;            
            updatedProfile.AlteredDate= DateTime.Now;
            updatedProfile.IP = ip;

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedProfile))//if entity is valid save. 
            {
                //add profile and commit changes
                _provinceRepository.Merge(currentProfile, updatedProfile);
                _provinceRepository.UnitOfWork.Commit();
                return updatedProfile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedProfile));
        }

        #endregion province

        #region area office

        /// <summary>
        /// Save Profile
        /// </summary>
        /// <param name="profile"></param>
        Area SaveAreaOffice(Area profile)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(profile))//if entity is valid save. 
            {
                //add profile and commit changes
                _areaOfficeRepository.Add(profile);                
                _areaOfficeRepository.UnitOfWork.Commit();
                return profile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profile));
        }

        /// <summary>
        /// Update existing Profile
        /// </summary>
        /// <param name="profile"></param>
        Area UpdateAreaOffice(Area currentProfile, Area updatedProfile,string ip="20.30.40.50")
        {
            updatedProfile.CreatedDate = currentProfile.CreatedDate;
            updatedProfile.AlteredDate = DateTime.Now;
            updatedProfile.IP = ip;
            updatedProfile.ProvinceId = currentProfile.ProvinceId;

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedProfile))//if entity is valid save. 
            {
                //add profile and commit changes
                _areaOfficeRepository.Merge(currentProfile, updatedProfile);
                _areaOfficeRepository.UnitOfWork.Commit();
                return updatedProfile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedProfile));
        }

        #endregion area office

        #region primary substaion

        /// <summary>
        /// Save Profile
        /// </summary>
        /// <param name="profile"></param>
        SubStation SavePrimarySubstation(SubStation profile)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(profile))//if entity is valid save. 
            {
                //add profile and commit changes
                _primarySubsationRepository.Add(profile);
                _primarySubsationRepository.UnitOfWork.Commit();
                return profile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profile));
        }

        /// <summary>
        /// Update existing Profile
        /// </summary>
        /// <param name="profile"></param>
        SubStation UpdatePrimarySubstation(SubStation currentProfile, SubStation updatedProfile, string ip="20.30.40.50")
        {
            updatedProfile.CreatedDate = currentProfile.CreatedDate;            
            updatedProfile.AlteredDate= DateTime.Now;
            updatedProfile.IP=ip;

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedProfile))//if entity is valid save. 
            {
                //add profile and commit changes
                _primarySubsationRepository.Merge(currentProfile, updatedProfile);
                _primarySubsationRepository.UnitOfWork.Commit();
                return updatedProfile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedProfile));
        }

        #endregion primary substaion

        #region Supplier

        /// <summary>
        /// Save Profile
        /// </summary>
        /// <param name="profile"></param>
        Supplier SaveSupplier(Supplier profile)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(profile))//if entity is valid save. 
            {
                //add profile and commit changes
                _supplierRepository.Add(profile);
                _supplierRepository.UnitOfWork.Commit();
                return profile;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(profile));
        }

        /// <summary>
        /// Update existing Profile
        /// </summary>
        /// <param name="profile"></param>
        Supplier UpdateSupplier(Supplier currentProfile, Supplier updatedObject, string ip = "")
        {
            updatedObject.CreatedDate = currentProfile.CreatedDate;
            updatedObject.AlteredDate = DateTime.Now;
            updatedObject.IP = ip;

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedObject))//if entity is valid save. 
            {
                //add profile and commit changes
                _supplierRepository.Merge(currentProfile, updatedObject);
                _supplierRepository.UnitOfWork.Commit();
                return updatedObject;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedObject));
        }

        #endregion primary substaion


        #region Meter

        /// <summary>
        /// Save Profile
        /// </summary>
        /// <param name="profile"></param>
        Meter SaveMeter(Meter meter)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(meter))//if entity is valid save. 
            {
                //add profile and commit changes
                _meterRepository.Add(meter);
                _meterRepository.UnitOfWork.Commit();
                return meter;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(meter));
        }

        /// <summary>
        /// Update existing Profile
        /// </summary>
        /// <param name="profile"></param>
        Meter UpdateMeter(Meter currentProfile, Meter updatedObject, string ip = "")
        {
            updatedObject.CreatedDate = currentProfile.CreatedDate;
            updatedObject.AlteredDate = DateTime.Now;
            updatedObject.IP = ip;

            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(updatedObject))//if entity is valid save. 
            {
                //add profile and commit changes
                _meterRepository.Merge(currentProfile, updatedObject);
                _meterRepository.UnitOfWork.Commit();
                return updatedObject;
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(updatedObject));
        }

        #endregion primary substaion



        #endregion Provate methods

    }
}
