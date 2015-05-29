using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EMS.DTO;
using EMS.Manager.Contract;

namespace Web.Controllers
{

    public class HomeController : Controller
    {
        #region Global declaration

        private readonly ILocationManager _locationManager;

        #endregion Global declaration

        #region Constructor

        public HomeController(ILocationManager locMgr)
        {
            _locationManager = locMgr;
        }

        public HomeController()
        {

        }

        #endregion Constructor

        #region Public Methods


        #region Province
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllProvinces()
        {
            var profiles = _locationManager.FindProvinces(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllProvincesAreas()
        {
            var profiles = _locationManager.GetAllProvincesAreas(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllLocationDetails()
        {
            var profiles = _locationManager.GetAllLocationDetails(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get all Province Names 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllProvinceNames()
        {
            var profiles = _locationManager.FindProvinces(0, 20,true).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }


            

        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetProvinceById(int id)
        {
            var profile = _locationManager.FindProvinceById(id);
            return this.Json(profile, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new Profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SaveProvinceInformation(ProvinceDTO profileDTO)
        {

            _locationManager.SaveProvinceInformation(profileDTO, Request.UserHostAddress.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdateProvinceInformation(int id, ProvinceDTO profileDTO)
        {
            _locationManager.UpdateProvinceInformation(id, profileDTO, Request.UserHostAddress.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void RemoveProvince(int id)
        {
            try
            {
                if (id != 0)
                {
                    _locationManager.DeleteProvince(id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }



        #endregion Province 
        
        #region AreaOffice
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllAreaOffices()
        {
            var profiles = _locationManager.FindAreaOffices(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetAreaOfficeById(int id)
        {
            var profile = _locationManager.FindAreaOfficeById(id);
            return this.Json(profile, JsonRequestBehavior.AllowGet);
        }

    

        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetAreasByProvinceId(int id)
        {
            //FindAreaOfficeByProvinceId
            var areaNames = _locationManager.FindAreaOfficeByProvinceId(id);
            return this.Json(areaNames, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new Profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SaveAreaOfficeInformation(AreaDTO objDTO)
        {

            _locationManager.SaveAreaOfficeInformation(objDTO);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdateAreaOfficeInformation(int id, AreaDTO objDTO)
        {
            _locationManager.UpdateAreaOfficeInformation(id, objDTO);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void DeleteAreaOffice(int id)
        {
            try
            {
                if (id != 0)
                {
                    _locationManager.DeleteAreaOffice(id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }



        #endregion area Office 

        #region Substation
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllSubstations()
        {
            var profiles = _locationManager.FindPrimarySubstations(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult FindPrimarySubstationById(int pageIndex, int pageCount)
        {
            var profile = _locationManager.FindPrimarySubstations(pageIndex, pageCount);
            return this.Json(profile, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetSubStationById(int id)
        {
            var profile = _locationManager.FindPrimarySubstationById(id);
            return this.Json(profile, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new Profile
        /// </summary>
        /// <param name="SubstationDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SavePrimarySubstationInformation(SubStationDTO substationDTO)
        {
            _locationManager.SavePrimarySubstationInformation(substationDTO, Request.UserHostAddress.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="substationDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdatePrimarySubstationInformation(int id, SubStationDTO ProfileDTO)
        {
            _locationManager.UpdatePrimarySubstationInformation(id, ProfileDTO);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void DeletePrimarySubstation(int id)
        {
            try
            {
                if (id != 0)
                {
                    _locationManager.DeletePrimarySubstation(id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }



        #endregion Substation

        #region Supplier
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllSuppliers()
        {
            var profiles = _locationManager.FindSuppliers(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Province Names 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllSupplierNames()
        {
            var profiles = _locationManager.FindSuppliers(0, 20, true).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetSupplierById(int id)
        {
            var profile = _locationManager.FindSupplierById(id);
            return this.Json(profile, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new Profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SaveSupplierInformation(SupplierDTO supplierDTO)
        {
            _locationManager.SaveSupplierInformation(supplierDTO, Request.UserHostAddress.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdateSupplierInformation(int id, SupplierDTO supplierDTO)
        {
            _locationManager.UpdateSupplierInformation(id, supplierDTO, Request.UserHostAddress.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void DeleteSupplier(int id)
        {
            try
            {
                if (id != 0)
                {
                    _locationManager.DeleteSupplier(id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }



        #endregion Supplier 

        #region Meter
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllMeters()
        {
            var profiles = _locationManager.FindMeters(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Meter Names 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllMeterNames()
        {
            var profiles = _locationManager.FindMeters(0, 20, true).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetMeterById(int id)
        {
            var profile = _locationManager.FindMeterById(id);
            return this.Json(profile, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new Profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SaveMeterInformation(MeterDTO profileDTO)
        {

            _locationManager.SaveMeterInformation(profileDTO, Request.UserHostAddress.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdateMeterInformation(int id, MeterDTO profileDTO)
        {
            _locationManager.UpdateMeterInformation(id, profileDTO, Request.UserHostAddress.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void DeleteMeter(int id)
        {
            try
            {
                if (id != 0)
                {
                    _locationManager.DeleteMeter(id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }



        #endregion Meter 

        #region Meter Reading
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllMetersReading()
        {
            var profiles = _locationManager.FindMeterReadings(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        #endregion Meter reading

        #region Report
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllReports()
        {
            var profiles = _locationManager.FindReports(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        #endregion Report

        #endregion Public Methods

        #region Views

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProvinceCreateEdit()
        {
            return View();
        }
        public ActionResult AreaCreateEdit()
        {
            return View();
        }

        public ActionResult ProvinceDetails()
        {
            return View();
        }
        public ActionResult AreaDetails()
        {
            return View();
        }

        public ActionResult SubStationDetails()
        {
            return View();
        }

        public ActionResult SubStationCreateEdit()
        {
            return View();
        }

        public ActionResult SupplierDetails()
        {
            return View();
        }

        public ActionResult SupplierCreateEdit()
        {
            return View();
        }

        public ActionResult MetersDetails()
        {
            return View();
        }

        public ActionResult MeterCreateEdit()
        {
            return View();
        }
        public ActionResult DashBoardDetails()
        {
            return View();
        }

        public ActionResult MeterReadingDetails()
        {
            return View();
        }

        public ActionResult ReportDetails()
        {
            return View();
        }


        #endregion Views

    }
}
