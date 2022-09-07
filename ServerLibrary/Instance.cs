using BankServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    public class Instance
    {
        private static BankServerInterface bankServerInterface;

        public static BankServerInterface getInterface()
        {
            ChannelFactory<BankServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/BankService";
            foobFactory = new ChannelFactory<BankServerInterface>(tcp, URL);
            bankServerInterface = foobFactory.CreateChannel();
            return bankServerInterface;
        }

    }
}
