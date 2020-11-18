using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Christoc.Modules.Chart.Models
{
    public class GetPerson
    {
        public int Id { get; set; }
        public CityAll IdCity { get; set; }
        public string CityName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string TenBieuDo { get; set; }
        public string MoTaBieuDo { get; set; }
        public string TenX { get; set; }
        public string TenY { get; set; }

    }
    public enum CityAll
    {
        Male,
        Female
    }
}