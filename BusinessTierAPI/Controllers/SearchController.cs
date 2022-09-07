using APIClasses;
using BankServer;
using ServerLibrary;
using System.Net;
using System.ServiceModel;
using System.Web.Http;


namespace BusinessTierAPI.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        
        [HttpPost]
        public IHttpActionResult Post([FromBody]SearchData data)
        {
            BankServerInterface foob = Instance.getInterface();
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
