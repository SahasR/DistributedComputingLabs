using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGUI.Models;

namespace WebGUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home Page";

            RestClient restClient = new RestClient("http://localhost:54227/");
            RestRequest restRequest = new RestRequest("api/StudentTables/", Method.Get);
            RestResponse restReponse = restClient.Execute(restRequest);

            List<StudentTable> students = JsonConvert.DeserializeObject<List<StudentTable>>(restReponse.Content);

            return View(students);
        }
    }
}
