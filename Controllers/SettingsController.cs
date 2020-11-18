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
using DotNetNuke.Entities.Modules;
using System;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

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
            //var settings = new Models.Settings();
            //settings.Setting1 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Chart_Setting1", true);
            //settings.Setting2 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Chart_Setting2", System.DateTime.Now);
            //settings.IdCity = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Chart_IdCity", 1);
            var settingsChart = new Models.GetPerson();
            settingsChart.CityName = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Chart_CityName", "Hue");
            settingsChart.Age = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Chart_Age", 1);
            ViewBag.Cities = ItemManager.Instance.Cities();
            ViewBag.ListField = ItemManager.Instance.GetFields();
            return View(settingsChart);
        }

        [HttpGet]
        public JsonResult GetPersons(int id)
        {
            var person = ItemManager.Instance.GetPersons(id);
            return Json(new { data = JsonConvert.SerializeObject(person, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPersonById(int id)
        {
            var person = ItemManager.Instance.GetPersonById(id);
            return Json(new { data = JsonConvert.SerializeObject(person, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
        }

       
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supportsTokens"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Settings(Models.GetPerson settingsChart)
        {
            
            //ModuleContext.Configuration.ModuleSettings["Chart_Setting1"] = settings.Setting1.ToString();
            //ModuleContext.Configuration.ModuleSettings["Chart_Setting2"] = settings.Setting2.ToUniversalTime().ToString("u");
            ModuleContext.Configuration.ModuleSettings["Chart_IdCity2"] = settingsChart.IdCity.ToString();
            ModuleContext.Configuration.ModuleSettings["Chart_CityName"] = settingsChart.CityName.ToString();
            ModuleContext.Configuration.ModuleSettings["Chart_Age"] = settingsChart.Age.ToString();
            ModuleContext.Configuration.ModuleSettings["Chart_TenBieuDo"] = settingsChart.TenBieuDo.ToString();
            ModuleContext.Configuration.ModuleSettings["Chart_MoTaBieuDo"] = settingsChart.MoTaBieuDo.ToString();
            ModuleContext.Configuration.ModuleSettings["Chart_TenX"] = settingsChart.TenX.ToString();
            ModuleContext.Configuration.ModuleSettings["Chart_Teny"] = settingsChart.TenY.ToString();

            return RedirectToDefaultRoute();
        }
        
    }
}