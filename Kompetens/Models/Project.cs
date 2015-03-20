using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetens.Models
{
    public class Project
    {
        public string name { get; set; }
        public string problem { get; set; }
        public string solution { get; set; }
        public string result { get; set; }
        public int hours { get; set; }
    }
}