using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Security.Principal;
using WebGUI.Models;

namespace WebGUI.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(int id)
        {
            RestClient restClient = new RestClient("http://localhost:51641/");
            RestRequest restRequest = new RestRequest("api/values/{index}", Method.Get);
            restRequest.AddUrlSegment("index", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] StudentTable account)
        {
            RestClient restClient = new RestClient("http://localhost:51641/");
            RestRequest request = new RestRequest("api/data/");
            request.AddObject(account);
            RestResponse response = restClient.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response.Content);
            } else
            {
                return BadRequest(response.Content);
            }
        }
    }
}
