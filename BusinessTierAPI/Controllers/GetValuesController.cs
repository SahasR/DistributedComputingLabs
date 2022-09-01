using APIClasses;
using BankServer;
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
        private BankServerInterface connect()
        {
            ChannelFactory<BankServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/BankService";
            foobFactory = new ChannelFactory<BankServerInterface>(tcp, URL);
            BankServerInterface foob = foobFactory.CreateChannel();
            return foob;
        }

        [Route("{index}")]
        [HttpGet]

        public DataIntermed Get(int index)
        {
            BankServerInterface foob = connect();
            
            string fName = null, lName = null;
            byte[] bitmapBytes;
            int bal = 0;
            uint acct = 0, pin = 0;

            foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out bitmapBytes);

            return new DataIntermed(bal, acct, pin, fName, lName, bitmapBytes);
        }
    }
}
