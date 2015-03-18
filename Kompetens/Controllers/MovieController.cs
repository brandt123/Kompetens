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
    public class MovieController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult SearchMovie(string q)
        {

            var data = WebApiConfig.GraphClient.Cypher
               .Match("(m:Movie)")
               .Where("m.title =~ {title}")
               .WithParam("title", "(?i).*" + q + ".*")
               .Return<Movie>("m")
               .Results.ToList();

            var data2 = WebApiConfig.GraphClient.Cypher
                .Match("(a)-[r]-(b)")
                .Where("a.title =~ {title}")
                .WithParam("title", "(?i).*" + q + ".*")
                .ReturnDistinct(r => new { Type = r.Type() })
                .Results;

            return Ok(data.Select(c => new { movie = c }));
        }
    }
}
