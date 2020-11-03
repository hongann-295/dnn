/*
' Copyright (c) 2020 Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Collections;
using System.Web.Mvc;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using Newtonsoft.Json;
using Christoc.Modules.Chart.Components;
using Christoc.Modules.Chart.Models;
using System.Collections.Generic;

namespace Christoc.Modules.Chart.Controllers
{
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
    [DnnHandleError]
    public class SettingsController : DnnController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Settings()
        {
            var settings = new Models.Settings();
            settings.Setting1 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Chart_Setting1", false);
            settings.Setting2 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Chart_Setting2", System.DateTime.Now);
            ViewBag.Cities = ItemManager.Instance.Cities();
            ViewBag.SelectY = ItemManager.Instance.GetCharts();
            return View(settings);
        }

        [HttpGet]
        public JsonResult GetCity()
        {
            var cities = ItemManager.Instance.Cities();
            return Json(new { data = JsonConvert.SerializeObject(cities, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPersons(int id)
        {
            var person = ItemManager.Instance.GetPersons(id);
            return Json(new { data = JsonConvert.SerializeObject(person, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult GetResults()
        //{
        //    var listCharts = ItemManager.Instance.GetCharts();
        //    Gender gender = new Gender();
        //    gender.Cities1 = new List<City>();
        //    gender.Cities2 = new List<City>();
        //    gender.Cities3 = new List<City>();
        //    foreach (var item in listCharts)
        //    {
        //        City city = new City();
        //        if (item.Gender == "Male")
        //        {
        //            city.IdCity = item.IdCity;
        //            city.CityName = item.CityName;
        //            city.Amount = item.Amount;
        //            gender.Cities1.Add(city);
        //        }
        //        if (item.Gender == "Female")
        //        {
        //            city.IdCity = item.IdCity;
        //            city.CityName = item.CityName;
        //            city.Amount = item.Amount;
        //            gender.Cities2.Add(city);
        //        }
        //        if (item.Gender == "Other")
        //        {
        //            city.IdCity = item.IdCity;
        //            city.CityName = item.CityName;
        //            city.Amount = item.Amount;
        //            gender.Cities3.Add(city);
        //        }
        //    }
        //    return Json(new { gender = JsonConvert.SerializeObject(gender, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supportsTokens"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Settings(Models.Settings settings)
        {
            ModuleContext.Configuration.ModuleSettings["Chart_Setting1"] = settings.Setting1.ToString();
            ModuleContext.Configuration.ModuleSettings["Chart_Setting2"] = settings.Setting2.ToUniversalTime().ToString("u");

            return RedirectToDefaultRoute();
        }
    }
}