using DatabaseAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Description;

namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        [HttpDelete]
        [Route("{index}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteAccount(int index)
        {
            Debug.WriteLine("Hello I am here!");
            RestClient restClient = new RestClient("http://localhost:54227/");
            RestRequest restRequest = new RestRequest("api/StudentTables/" + index);
            RestResponse restResponse = restClient.Delete(restRequest);
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult InsertAccount(StudentTable account)
        {
         
            RestClient restClient = new RestClient("http://localhost:54227/");
            RestRequest restRequest = new RestRequest("api/StudentTables/");
            RestResponse restResponse = restClient.Get(restRequest);
            IEnumerable<StudentTable> accounts = JsonConvert.DeserializeObject<IEnumerable<StudentTable>>(restResponse.Content);
            int count = accounts.Count();
            if (account.id != 0)
            {
                restRequest = new RestRequest("api/StudentTables/");
                restRequest.AddBody(account);
                try
                {
                    restResponse = restClient.Post(restRequest);
                    return Ok();
                } catch (HttpRequestException e)
                {
                    return BadRequest();
                }     
            }
            else
            {
                StudentTable newAccount;
                if (count == 0)
                {
                    newAccount = new StudentTable(1, account.firstName, account.lastName, account.balance, account.acctNo, account.pin, account.image);
                }
                else
                {
                    StudentTable lastAccount = accounts.Last();
                    newAccount = new StudentTable(lastAccount.id + 1, account.firstName, account.lastName, account.balance, account.acctNo, account.pin, account.image);
                }
                restRequest = new RestRequest("api/StudentTables/");
                restRequest.AddBody(newAccount);
                restResponse = restClient.Post(restRequest);
                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
