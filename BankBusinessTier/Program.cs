using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankBusinessTier;

namespace BankBusinessTier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Bank Business Server!");
            //This is the actual host service System
            ServiceHost host;
            //This represents a tcp/ip binding in the Windows Network Stack
            NetTcpBinding tcp = new NetTcpBinding();
            //Bind Server to the implementation of BankServer
            host = new ServiceHost(typeof(BusinessServer));
            //Present the publicly accessible interface to the client. 0.0.0.0 tells .net to accept on any interface.
            //:8100 means this will use port 8100. DataService is a name for the actual service, this can be any string.
            host.AddServiceEndpoint(typeof(BusinessServerInterface), tcp, "net.tcp://0.0.0.0:8200/BankBusinessService");
            //And open the host for business!
            host.Open();
            Console.WriteLine("System is online!");
            Console.ReadLine();
            //Don't forget to close the host after you're done!
            host.Close();
        }
    }
}
