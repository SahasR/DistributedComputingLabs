using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

            restRequest = new RestRequest("api/Accounts/");
            restResponse = restClient.Get(restRequest);
            IEnumerable<Account> accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(restResponse.Content);
            if (accounts.Count() == 0)
            {
                for (int i = 1; i <= 100; i++)
                {
                    account = GetNextAccount(i, i);
                    restRequest = new RestRequest("api/Accounts/");
                    restRequest.AddBody(account);
                    restResponse = restClient.Post(restRequest);
                }
            } else
            {
                Account lastAccount = accounts.Last();

                for (int i = lastAccount.Id + 1; i <= lastAccount.Id + 100; i++)
                {
                    account = GetNextAccount(i, i);
                    restRequest = new RestRequest("api/Accounts/");
                    restRequest.AddBody(account);
                    restResponse = restClient.Post(restRequest);
                }
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

        private Bitmap GetProfilePic()
        {
            Random random = new Random();
            var image = new Bitmap(64, 64);

            for (var x = 0; x < 64; x++)
            {
                for (var y = 0; y < 64; y++)
                {
                    image.SetPixel(x, y, Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                }
            }
            Bitmap profilePic = image;
            return profilePic;
        }

        public Account GetNextAccount(int seed, int id)
        {
            uint pin = GetPIN(seed);
            uint acctNo = GetAcctNo(seed);
            string firstName = GetFirstName(seed);
            string lastName = GetLastName(seed);
            decimal balance = GetBalance(seed);
            Bitmap image = GetProfilePic();
            string bitmapImage = BitmapToString(image);
            return new Account(id, firstName, lastName, balance, acctNo.ToString(), pin.ToString(), bitmapImage);
        }

        private string BitmapToString(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                byte[] byteImage = stream.ToArray();
                string image = Convert.ToBase64String(byteImage);
                return image;
            }
        }
    }

    
}
