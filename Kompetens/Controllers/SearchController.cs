using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kompetens.Models;

namespace Kompetens.Controllers
{
    [RoutePrefix("searchcontroller")]
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search(string q)
        {
            var data3 = WebApiConfig.GraphClient.Cypher
               .Match("(n)")
                //.Where("m.name =~ {name}")
                //.WithParam("name", "(?i).*" + q + ".*")
                .Return(n => new { name = n.As<Project>().name, label = n.Labels() })
                .Results;

            /* ANVÄNDS INTE, MEN SPARA ÄNDÅ!
            var data2 = WebApiConfig.GraphClient.Cypher
                .Match("(a)-[r]-(b)")
                .Where("a.title =~ {title}")
                .WithParam("title", "(?i).*" + q + ".*")
                .ReturnDistinct(r => new { Type = r.Type() })
                .Results;
             */

            return Ok(data3);
            
        }

       

        [HttpGet]
        [Route("GetNodes")]
        public IHttpActionResult GetNodes() {

            var nodes = new List<object>();
            var rels = new List<object>();

            nodes.Add(new { name = "Matrix", group = 1 });
            nodes.Add(new { name = "Test", group = 2 });
            rels.Add(new { source = 1, target = 0, value = 1 });


            var data = WebApiConfig.GraphClient.Cypher
               .Match("(n)")
               .Return(n => new { name = n.As<Project>().name, label = n.Labels() })
               .Results;
            return Ok(new { nodes = nodes, links = rels });

        
        }
    }
}
