using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
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
            try
            {
                RestResponse response = restClient.Post(request);
                return Ok(response.Content);
            } catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            RestClient restClient = new RestClient("http://localhost:51641/");
            RestRequest request = new RestRequest("api/data/" + id);
            RestResponse restResponse = restClient.Delete(request);
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(restResponse.Content);
            } else{
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] StudentTable account)
        {
            RestClient restClient = new RestClient("http://localhost:51641/");
            RestRequest request = new RestRequest("api/data/" + account.id);
            RestResponse response = restClient.Delete(request);
     
            request = new RestRequest("api/data/");
            request.AddObject(account);
            try
            {
                response = restClient.Post(request);
                return Ok(response.Content);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Generate()
        {
            Debug.WriteLine("Hello I am here at Generate!");
            RestClient restClient = new RestClient("http://localhost:51641/");
            RestRequest request = new RestRequest("api/generator/");
            RestResponse restResponse = restClient.Put(request);
            return Ok(restResponse.Content);
        }
    }
}
