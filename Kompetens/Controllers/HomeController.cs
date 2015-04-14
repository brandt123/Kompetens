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

        public class User
        {
            public string Name { get; set; }
            public string Problem { get; set; }
            public string Solution { get; set; }
            public string Result { get; set; }
        }

        [HttpPost]
        public ActionResult About(RegisterViewModel vm) {
            Response.Write("DET FUNKAR");
                
            var newUser = new User { Name = vm.Name, Problem = vm.Problem, Solution = vm.Solution, Result = vm.Result };
            WebApiConfig.GraphClient.Cypher
                .Merge("(user:User { Name: {Name} })")
                .Set("user = {newUser}")
                .WithParams(new
                {
                    Name = newUser.Name,
                    newUser
                })
                .ExecuteWithoutResults();

            return View();
        }
    }
}