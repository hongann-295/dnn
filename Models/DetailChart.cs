using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Christoc.Modules.Chart.Models
{
    public class DetailChart
    {
        public int Id { get; set; }
        public string TenBieuDo { get; set; }
        public string MoTaBieuDo { get; set; }
        public string TenAxisX { get; set; }
        public string TenAxisY { get; set; }
    }
}