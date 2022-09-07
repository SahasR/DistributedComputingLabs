using BankServer;
using ServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace BusinessTierAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        
        public int Get()
        {
            BankServerInterface foob = Instance.getInterface();
            return foob.GetNumEntries();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
