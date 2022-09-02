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
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
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


        [HttpPost]
        public IHttpActionResult Post([FromBody]SearchData data)
        {
            BankServerInterface foob = connect();
            uint accountNumber;
            uint pinNumber;
            int currentBalance;
            string firstName;
            string lastName;
            byte[] tempImage;

            int numEntries = foob.GetNumEntries();
            for (int i = 0; i < numEntries; i++)
            {
                try
                {
                    foob.GetValuesForEntry(i, out accountNumber, out pinNumber, out currentBalance, out firstName, out lastName, out tempImage);
                    if (lastName.ToLower().Contains(data.searchStr))
                    {
                        DataIntermed dataIntermed = new DataIntermed(currentBalance, accountNumber, pinNumber, firstName, lastName, tempImage);
                        return Ok(dataIntermed);
                    }
                }
                catch (FaultException<ServerFailureException> ex) { }
            }
            return Content(HttpStatusCode.NotFound, new NotFoundException("Couldn't find " + data.searchStr));
        }
    }
}
