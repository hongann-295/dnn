﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Christoc.Modules.Chart.Models
{
    public class GetPerson
    {
        public int Id { get; set; }
        public int IdCity { get; set; }
        public string CityName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}