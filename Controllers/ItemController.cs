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

using System;
using System.Linq;
using System.Web.Mvc;
using Christoc.Modules.Chart.Components;
using Christoc.Modules.Chart.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.Linq;

namespace Christoc.Modules.Chart.Controllers
{
    [DnnHandleError]
    public class ItemController : DnnController
    {

        public ActionResult Delete(int itemId)
        {
            ItemManager.Instance.DeleteItem(itemId, ModuleContext.ModuleId);
            return RedirectToDefaultRoute();
        }

        public ActionResult Edit(int itemId = -1)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var userlist = UserController.GetUsers(PortalSettings.PortalId);
            var users = from user in userlist.Cast<UserInfo>().ToList()
                        select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };

            ViewBag.Users = users;

            var item = (itemId == -1)
                 ? new Item { ModuleId = ModuleContext.ModuleId }
                 : ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);

            return View(item);
        }

        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (item.ItemId == -1)
            {
                item.CreatedByUserId = User.UserID;
                item.CreatedOnDate = DateTime.UtcNow;
                item.LastModifiedByUserId = User.UserID;
                item.LastModifiedOnDate = DateTime.UtcNow;

                ItemManager.Instance.CreateItem(item);
            }
            else
            {
                var existingItem = ItemManager.Instance.GetItem(item.ItemId, item.ModuleId);
                existingItem.LastModifiedByUserId = User.UserID;
                existingItem.LastModifiedOnDate = DateTime.UtcNow;
                existingItem.ItemName = item.ItemName;
                existingItem.ItemDescription = item.ItemDescription;
                existingItem.AssignedUserId = item.AssignedUserId;

                ItemManager.Instance.UpdateItem(existingItem);
            }

            return RedirectToDefaultRoute();
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
        public ActionResult Index()
        {
            var items = ItemManager.Instance.GetItems(ModuleContext.ModuleId);
            return View(items);
        }

        [HttpGet]
        public JsonResult GetResults()
        {
            var listCharts = ItemManager.Instance.GetCharts();
            Gender gender = new Gender();
            gender.Cities1 = new List<City>();
            gender.Cities2 = new List<City>();
            gender.Cities3 = new List<City>();
            foreach (var item in listCharts)
            {
                City city = new City();
                if (item.Gender == "Male")
                {
                    city.IdCity = item.IdCity;
                    city.CityName = item.City;
                    city.Amount = item.Amount;
                    gender.Cities1.Add(city);
                }
                if (item.Gender == "Female")
                {
                    city.IdCity = item.IdCity;
                    city.CityName = item.City;
                    city.Amount = item.Amount;
                    gender.Cities2.Add(city);
                }
                if (item.Gender == "Other")
                {
                    city.IdCity = item.IdCity;
                    city.CityName = item.City;
                    city.Amount = item.Amount;
                    gender.Cities3.Add(city);
                }
            }
            return Json(new { gender = JsonConvert.SerializeObject(gender, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public JsonResult Save(Employee employee)
        {
            ItemManager.Instance.SaveEmployee(employee);
            Employee employees = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Gender = employee.Gender
            };
            return Json(employees, JsonRequestBehavior.AllowGet);
            //return Json(new { data = JsonConvert.SerializeObject(employees, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployees()
        {
            var employee = ItemManager.Instance.GetEmployees();
           
            return View(employee);
        }

        [HttpGet]
        public JsonResult GetEmployee(string employeeId)
        {

            try
            {
                var employee = ItemManager.Instance.GetEmployee(int.Parse(employeeId));
                return Json(new { data = JsonConvert.SerializeObject(employee, Formatting.Indented) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        [HttpGet]
        public JsonResult DeleteEmployee(string Id)
        {

            try
            {
                var delEm = ItemManager.Instance.DeleteEmployee(int.Parse(Id));
                return Json(new { data = JsonConvert.SerializeObject(delEm, Formatting.Indented) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //public ActionResult CreateChart(DetailChart detailChart)
        //{
        //    ItemManager.Instance.CreateChart(detailChart);
        //    return RedirectToDefaultRoute();

        //}
        
        //[HttpPost]
        //public ActionResult Create(DetailChart detailChart)
        //{
              
        //    if (ModelState.IsValid)
        //    {
                
               
        //        return RedirectToAction("Index");
        //    }

        //    return View(detailChart);
        //}
    }
}



//List<GetChart> charts = new List<GetChart>();
//if (listCharts != null && listCharts.Count() > 0)
//{
//    foreach (var item in listCharts)
//    {
//        charts.Add(new GetChart { Gender = item.Gender, Amount = item.Amount });
//    }
//}


//try
//{
//    List<GetChart> charts = new List<GetChart>();
//    charts.Add(new GetChart { Gender = "Male", Amount = 5 });
//    charts.Add(new GetChart { Gender = "Female", Amount = 30 });
//    charts.Add(new GetChart { Gender = "Other", Amount = 70 });

//    return Json(new { data = JsonConvert.SerializeObject(charts, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
//}
//catch (Exception e)
//{
//    throw e;
//}


//[HttpPost]
//public JsonResult SaveEmployee(Employee employee)
//{
//   return

//}
//public JsonResult GetEmployees()
//{
//    var listEmployee = ItemManager.Instance.GetEmployees();
//    return Json(new { data = JsonConvert.SerializeObject(listEmployee, Formatting.Indented) }, JsonRequestBehavior.AllowGet);
//}