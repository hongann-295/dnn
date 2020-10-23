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

using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Christoc.Modules.Chart.Models;
using System.Collections;
using System;
using System.Linq;

namespace Christoc.Modules.Chart.Components
{
    interface IItemManager
    {
        void CreateItem(Item t);
        void DeleteItem(int itemId, int moduleId);
        void DeleteItem(Item t);
        IEnumerable<Item> GetItems(int moduleId);
        Item GetItem(int itemId, int moduleId);
        void UpdateItem(Item t);
        IEnumerable<GetChart> GetCharts();
        void SaveEmployee(Employee employee);
        //void UpdateEmployee(Employee employee);
        Employee GetEmployee(int employeeId);
        IEnumerable<Employee> GetEmployees();
        DeleteResult DeleteEmployee(int Id);
    }

    class ItemManager : ServiceLocator<IItemManager, ItemManager>, IItemManager
    {
        public void CreateItem(Item t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                rep.Insert(t);
            }
        }

        public void DeleteItem(int itemId, int moduleId)
        {
            var t = GetItem(itemId, moduleId);
            DeleteItem(t);
        }

        public void DeleteItem(Item t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                rep.Delete(t);
            }
        }

        public IEnumerable<Item> GetItems(int moduleId)
        {
            IEnumerable<Item> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                t = rep.Get(moduleId);
            }
            return t;
        }

        public Item GetItem(int itemId, int moduleId)
        {
            Item t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public void UpdateItem(Item t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                rep.Update(t);
            }
        }

        protected override System.Func<IItemManager> GetFactory()
        {
            return () => new ItemManager();
        }

        public IEnumerable<GetChart> GetCharts()
        {
                using (IDataContext ctx = DataContext.Instance())
                {
                    return ctx.ExecuteQuery<GetChart>(System.Data.CommandType.StoredProcedure, String.Format("Sp_Gender2"));
                }
            
        }

        public Employee GetEmployee(int employeeId)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IEnumerable<Employee> x= ctx.ExecuteQuery<Employee>(System.Data.CommandType.StoredProcedure, String.Format( "Sp_GetEmployeeById2 {0}", employeeId));
                return x.FirstOrDefault();
            }

        }

        public IEnumerable<Employee> GetEmployees()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<Employee>(System.Data.CommandType.StoredProcedure, String.Format("Sp_GetEmployee2"));
            }

        }

        public DeleteResult DeleteEmployee(int Id)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IEnumerable<DeleteResult> y = ctx.ExecuteQuery<DeleteResult>(System.Data.CommandType.StoredProcedure, String.Format("Sp_DeleteEmployee {0}", Id));
                return y.FirstOrDefault();
                //ctx.ExecuteScalar<Employee>(System.Data.CommandType.StoredProcedure, String.Format("Sp_DeleteEmployee {0}", Id));
            }
        }

        //public bool IItemManager.SaveEmployee(Employee employee)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        ctx.ExecuteQuery<Employee>(System.Data.CommandType.StoredProcedure, String.Format("Sp_SaveEmployee2"));
        //        return true; //ctx.ExecuteQuery<Employee>(System.Data.CommandType.StoredProcedure, String.Format("Sp_SaveEmployee2 {0}", new object[] {employee.Id, employee.Name, employee.Gender } ));
        //    }
        //}

        public void SaveEmployee(Employee employee)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.ExecuteQuery<Employee>(System.Data.CommandType.StoredProcedure, String.Format("Sp_SaveEmployee2"));
                
            }
        }
    }
}
