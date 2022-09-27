using APIClasses;
using DatabaseAPI.Models;
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

namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/values")]
    public class GetValuesController : ApiController
    {
       
        [Route("{index}")]
        [HttpGet]
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult Get(int index)
        {
            StudentTable account = null;
            RestClient restClient = new RestClient("http://localhost:54227/");
            RestRequest restRequest = new RestRequest("api/StudentTables/" + index.ToString());
            RestResponse restResponse = restClient.Get(restRequest);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                account = JsonConvert.DeserializeObject<StudentTable>(restResponse.Content);
                return Ok(account);
            } else
            {
                return NotFound();
            }
        }
    }
}
