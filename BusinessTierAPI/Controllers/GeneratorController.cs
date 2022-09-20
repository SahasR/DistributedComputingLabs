using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using WebDatabaseAPI.Models;

namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/generator")]
    public class GeneratorController : ApiController
    {
        //PUT api/generator
        [ResponseType(typeof(void))]
        [HttpPut]
        public void Put()
        {
            Account account;
            RestClient restClient = new RestClient("http://localhost:58308/");
            RestRequest restRequest;
            RestResponse restResponse;

            for (int i = 1; i <= 100; i++)
            {
                account = GetNextAccount(i, i);
                restRequest = new RestRequest("api/Accounts/");
                restRequest.AddBody(account);
                restResponse = restClient.Post(restRequest);
            }
        } 
        private string GetFirstName(int seed)
        {
            Random random = new Random(seed);
            string[] firstNames = { "Sahas", "Abinaya", "Venumi", "Kapila", "Anurudda", "Pathum", "Nalaka", "Renuja", "Nisal" };
            int randomNumber = random.Next(0, 9);
            string firstName = firstNames[randomNumber];
            return firstName;
        }

        private string GetLastName(int seed)
        {
            Random random = new Random(seed);
            string[] lastNames = { "Gunasekara", "Sritharan", "Kaluarachchi", "Kaluarachchi", "Padeniya", "Nissanka", "Godahewa", "Rajapakse", "Seneviratne" };
            int randomNumber = random.Next(0, 9);
            string lastName = lastNames[randomNumber];
            return lastName;
        }

        private uint GetPIN(int seed)
        {
            Random random = new Random(seed);
            uint pin = (uint)random.Next(0, 10000);
            return pin;
        }

        private uint GetAcctNo(int seed)
        {
            Random random = new Random(seed);
            uint accNum = (uint)random.Next(0, 10000000);
            return accNum;
        }

        private int GetBalance(int seed)
        {
            Random random = new Random(seed);
            int balance = random.Next(0, 1000000000);
            return balance;
        }

        private string GetImage(int seed)
        {
            string[] locations = { "C:/Users/sahas/Source/Repos/DistributedComputingLab01/DatabaseImages/Images/1.jpg",
                "C:/Users/sahas/Source/Repos/DistributedComputingLab01/DatabaseImages/Images/2.png" };
            //When storing locations I had to store locations as Absolute Paths because this program will have a different working directory when in debug mode
            //And a different working directory when in final build executable. So these are two placeholder images, often there will be a seperate server with the images loaded
            //from a URL so it shouldn't be too bad, currently however it is stored inside the .dll file for the Databases.
            Random random = new Random(seed);
            string location = locations[random.Next(0, 2)];
            return location;
        }

        public Account GetNextAccount(int seed, int id)
        {
            uint pin = GetPIN(seed);
            uint acctNo = GetAcctNo(seed);
            string firstName = GetFirstName(seed);
            string lastName = GetLastName(seed);
            decimal balance = GetBalance(seed);
            String imageLocation = GetImage(seed);
            Bitmap image = new Bitmap(imageLocation, true);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();
            return new Account(id, firstName, lastName, balance, acctNo.ToString(), pin.ToString(), bytes);
        }
    }

    
}
