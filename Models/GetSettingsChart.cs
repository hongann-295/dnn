using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Christoc.Modules.Chart.Models
{
    public class GetSettingsChart
    {
        public int ModuleID { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
    }
}