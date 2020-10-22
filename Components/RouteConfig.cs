using DotNetNuke.Web.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Christoc.Modules.Chart.Components
{
    public class RouteConfig : IMvcRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapRoute("Chart", "default", "{controller}/{action}",
                new[] { "Christoc.Modules.Chart.Controllers" });


            //DataTable
           // ModelBinders.Binders.Add(typeof(DTParameterModel), new DTModelBinder());

            ////EO.Pdf 16.0.28
            //EO.Pdf.Runtime.AddLicense(
            //    "HOKybaq3wd+0dabw+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hny" +
            //    "ntzCnrWfWZekzQzrpeb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mk" +
            //    "BxDxrODz/+ihaq2ywc2faLWRm8ufWZfAwAzrpeb7z7iJWZeksefuq9vpA/Tt" +
            //    "n+ak9QzznrSmxdq2aKm0wuChWer58/D3qeD29h7ArbSmxdq2aKm0wuGhWe3p" +
            //    "Ax7oqOXBs+GhWabCnrWfWZekzR7ooOXlBSDxnrX2Au/VsKvcxxTgjcTc6BD4" +
            //    "qbvP3AH2drTAwB7ooOXlBSDxnrWRm+eupeDn9hnynrWRm3Xj7fQQ7azcwp61" +
            //    "n1mXpM0X6Jzc8gQQyJ21u8E=");
        }
    }
}