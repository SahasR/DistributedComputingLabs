using APIClasses;
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
    [RoutePrefix("api/values")]
    public class GetValuesController : ApiController
    {
       
        [Route("{index}")]
        [HttpGet]

        public DataIntermed Get(int index)
        {
            BankServerInterface foob = Instance.getInterface();
            
            string fName = null, lName = null;
            byte[] bitmapBytes;
            int bal = 0;
            uint acct = 0, pin = 0;

            foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out bitmapBytes);

            return new DataIntermed(bal, acct, pin, fName, lName, bitmapBytes);
        }
    }
}
