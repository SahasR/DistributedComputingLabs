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
    public class ValuesController : ApiController
    {
        // GET api/values
        public BankServerInterface connect()
        {
            ChannelFactory<BankServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/BankService";
            foobFactory = new ChannelFactory<BankServerInterface>(tcp, URL);
            BankServerInterface foob = foobFactory.CreateChannel();
            return foob;
        }
        public int Get()
        {
            BankServerInterface foob = connect();
            return foob.GetNumEntries();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
