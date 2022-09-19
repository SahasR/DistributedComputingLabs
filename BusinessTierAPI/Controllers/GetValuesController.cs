using APIClasses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;
using WebDatabaseAPI.Models;

namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/values")]
    public class GetValuesController : ApiController
    {
       
        [Route("{index}")]
        [HttpGet]
        [ResponseType(typeof(Account))]
        public IHttpActionResult Get(int index)
        {
            Account account = null;
            RestClient restClient = new RestClient("http://localhost:58308/");
            RestRequest restRequest = new RestRequest("api/Accounts/" + index.ToString());
            RestResponse restResponse = restClient.Get(restRequest);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                account = JsonConvert.DeserializeObject<Account>(restResponse.Content);
                return Ok(account);
            } else
            {
                return NotFound();
            }
        }
    }
}
