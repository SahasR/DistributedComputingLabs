using APIClasses;
using System.Net;
using System.ServiceModel;
using System.Web.Http;


namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        
        [HttpPost]
        public IHttpActionResult Post([FromBody]SearchData data)
        {
             
            return Content(HttpStatusCode.NotFound, new NotFoundException("Couldn't find " + data.searchStr));
        }
    }
}
