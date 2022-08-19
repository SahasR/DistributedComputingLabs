using BankServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Drawing;
using System.IO;

namespace BankBusinessTier
{
    internal class BusinessServer : BusinessServerInterface
    {
        private BankServerInterface foob;
        public BusinessServer()
        {
            ChannelFactory<BankServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection
            string URL = "net.tcp://localhost:8100/BankService";
            foobFactory = new ChannelFactory<BankServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }
        public int GetNumEntries()
        {
            return foob.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out byte[] bitmapBytes)
        {
            try
            {
                foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out bitmapBytes);
            }
            catch (FaultException<ServerFailureException> ex)
            {
                throw ex;
            }
        }
    }
}
