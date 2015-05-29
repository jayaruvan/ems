using EMS.DTO;
using EMS.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EMS.Web.Controllers
{
    public class LocationController : Controller
    {
        #region Global declaration

        private readonly ILocationManager _locationManager;

        #endregion Global declaration

        #region Constructor

        public LocationController(ILocationManager locMgr)
        {
            _locationManager = locMgr;
        }

        public LocationController()
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
            //Patch-- Javascript Always calls here..
            // I'll debug that later..
            if (profileDTO.Id > 0)
            {
                UpdateProvinceInformation(profileDTO.Id,profileDTO);
                               
            }
            else
            {
                _locationManager.SaveProvinceInformation(profileDTO);
            }
            
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
            _locationManager.UpdateProvinceInformation(id, profileDTO);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void DeleteProvince(int id)
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
        /// Create a new Profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SaveAreaOfficeInformation(AreaDTO objDTO)
        {
            //Patch-- Javascript Always calls here..
            // I'll debug that later..
            if (objDTO.Id > 0)
            {
                UpdateAreaOfficeInformation(objDTO.Id, objDTO);

            }
            else
            {
                _locationManager.SaveAreaOfficeInformation(objDTO);
            }

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
        
        #region PrimarySubstation
        /// <summary>
        /// Get all Profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPrimarySubstations()
        {
            var profiles = _locationManager.FindPrimarySubstations(0, 20).AsQueryable();
            return this.Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetPrimarySubstationById(int id)
        {
            var obj = _locationManager.FindPrimarySubstationById(id);
            return this.Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new Profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SavePrimarySubstationInformation(SubStationDTO objDTO)
        {
            //Patch-- Javascript Always calls here..
            // I'll debug that later..
            if (objDTO.Id > 0)
            {
                UpdatePrimarySubstationInformation(objDTO.Id, objDTO);

            }
            else
            {
                _locationManager.SavePrimarySubstationInformation(objDTO);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdatePrimarySubstationInformation(int id, SubStationDTO objDTO)
        {
            _locationManager.UpdatePrimarySubstationInformation(id, objDTO);
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



        #endregion primary substation


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
        #endregion Views
    }
}
