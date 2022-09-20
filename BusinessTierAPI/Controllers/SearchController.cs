using APIClasses;
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
using WebDatabaseAPI.Models;

namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/search")]

    public class SearchController : ApiController
    {
        
        [HttpPost]
        [ResponseType(typeof(Account))]
        public IHttpActionResult Post([FromBody]SearchData data)
        {
            RestClient restClient = new RestClient("http://localhost:58308/");
            RestRequest restRequest = new RestRequest("api/Accounts/");
            RestResponse restResponse = restClient.Get(restRequest);
            IEnumerable<Account> accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(restResponse.Content);
            foreach (Account account in accounts)
            {
                
                if (account.LastName.ToLower().Contains(data.searchStr.ToLower())){
                    return Ok(account);
                }
            }
            return NotFound();
        }
    }
}
