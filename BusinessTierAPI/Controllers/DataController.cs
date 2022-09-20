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
using WebDatabaseAPI.Models;

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
            RestClient restClient = new RestClient("http://localhost:58308/");
            RestRequest restRequest = new RestRequest("api/Accounts/" + index);
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
        public IHttpActionResult InsertAccount(Account account)
        {
         
            RestClient restClient = new RestClient("http://localhost:58308/");
            RestRequest restRequest = new RestRequest("api/Accounts/");
            RestResponse restResponse = restClient.Get(restRequest);
            IEnumerable<Account> accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(restResponse.Content);
            int count = accounts.Count();
            if (account.Id != 0)
            {
                restRequest = new RestRequest("api/Accounts/");
                restRequest.AddBody(account);
                restResponse = restClient.Post(restRequest);
                return Ok();
            }
            else
            {
                Account newAccount;
                if (count == 0)
                {
                    newAccount = new Account(1, account.FirstName, account.LastName, account.Balance, account.AcctNo, account.Pin, account.Image);
                }
                else
                {
                    Account lastAccount = accounts.Last();
                    newAccount = new Account(lastAccount.Id + 1, account.FirstName, account.LastName, account.Balance, account.AcctNo, account.Pin, account.Image);
                }
                restRequest = new RestRequest("api/Accounts/");
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
