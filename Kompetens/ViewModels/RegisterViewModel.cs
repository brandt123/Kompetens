using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetens.ViewModels
{
    public class RegisterViewModel
    {

        public string Name { get; set; }

        public string Problem { get; set; }

        public string Solution { get; set; }

        public string Result { get; set; }

        public string KundNamn { get; set; }

        public string Bestallare { get; set; }
    }

    public class KundViewModel 
    {
        public string KundNamn { get; set; }

        public string BestallareNamn { get; set; }
    
    }
}