using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kompetens.ViewModels;

namespace Kompetens.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var vm = new RegisterViewModel();

            return View(vm);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public class Projekt
        {
            public string Name { get; set; }
            public string Problem { get; set; }
            public string Solution { get; set; }
            public string Result { get; set; }
        }

        public class Kund
        {
            public string Namn { get; set; }

        }

        public class Bestallare
        {
            public string Namn { get; set; }

            public string Foretag { get; set; }

        }

        [HttpPost]
        public ActionResult About(RegisterViewModel vm) {
            Response.Write("DET FUNKAR");
                
            var nyProj = new Projekt { Name = vm.Name, Problem = vm.Problem, Solution = vm.Solution, Result = vm.Result };
            WebApiConfig.GraphClient.Cypher
                .Merge("(projekt:Projekt { Name: {Name} })")
                .Set("projekt = {nyProj}")
                .WithParams(new
                {
                    Name = nyProj.Name,
                    nyProj
                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Match("(kund:Kund)", "(projekt:Projekt)", "(bestallare:Bestallare)")
                .Where((Kund kund) => kund.Namn == vm.KundNamn)
                .AndWhere((Projekt projekt) => projekt.Name == vm.Name)
                .AndWhere((Bestallare bestallare) => bestallare.Namn == vm.Bestallare)
                .CreateUnique("kund-[:BESTALLT]->projekt")
                .CreateUnique("bestallare-[:BESTALLT_AV]->kund")
                .ExecuteWithoutResults(); 

            return View();
        }

        public ActionResult CreateTitleModal()
        {

            KundViewModel km = new KundViewModel();
            return PartialView("_PartialKund", km);
        }

        [HttpPost]
        public ActionResult SkapaNyKund(KundViewModel kvm)
        {
            var nyKund = new Kund { Namn = kvm.KundNamn};
            var nyBestallare = new Bestallare { Namn = kvm.BestallareNamn, Foretag = kvm.KundNamn };
            WebApiConfig.GraphClient.Cypher
                .Merge("(kund:Kund { Namn: {Namn} })")
                .Set("kund = {nyKund}")
                .WithParams(new
                {
                    Namn = nyKund.Namn,
                    nyKund
                    
                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Merge("(bestallare:Bestallare { Namn: {Namn} })")
                .Set("bestallare = {nyBestallare}")
                .WithParams(new
                {
                    Namn = nyBestallare.Namn,
                    nyBestallare

                })
                .ExecuteWithoutResults();
            
            WebApiConfig.GraphClient.Cypher
                .Match("(kund:Kund)", "(bestallare:Bestallare)")
                .Where((Kund kund) => kund.Namn == kvm.KundNamn)
                .AndWhere((Bestallare bestallare) => bestallare.Namn == kvm.BestallareNamn)
                .CreateUnique("kund-[:JOBBAR]->bestallare")
                .ExecuteWithoutResults(); 
            return View("Index");
        }
         
    }
}