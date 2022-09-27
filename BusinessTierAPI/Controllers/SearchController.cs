using APIClasses;
using DatabaseAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Description;

namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/search")]

    public class SearchController : ApiController
    {
        
        [HttpPost]
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult Post([FromBody]SearchData data)
        {
            RestClient restClient = new RestClient("http://localhost:54227/");
            RestRequest restRequest = new RestRequest("api/StudentTables/");
            RestResponse restResponse = restClient.Get(restRequest);
            IEnumerable<StudentTable> accounts = JsonConvert.DeserializeObject<IEnumerable<StudentTable>>(restResponse.Content);
            foreach (StudentTable account in accounts)
            {
                
                if (account.lastName.ToLower().Contains(data.searchStr.ToLower())){
                    return Ok(account);
                }
            }
            return NotFound();
        }
    }
}
