using BankServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

namespace BankBusinessTier
{
    internal class BusinessServer : BusinessServerInterface
    {
        private BankServerInterface foob;
        private uint LogNumber;
        private string client;
        public BusinessServer()
        {
            ChannelFactory<BankServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection
            string URL = "net.tcp://localhost:8100/BankService";
            foobFactory = new ChannelFactory<BankServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
            LogNumber = 0;
            Log("New Client Conencted!");

        }
        public int GetNumEntries()
        {
            return foob.GetNumEntries();
            Log("Fetched the number of entries");
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out byte[] bitmapBytes)
        {
            try
            {
                foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out bitmapBytes);
                Log("Fetched the Entry " + index + " : " + fName + " : " + lName);
            }
            catch (FaultException<ServerFailureException> ex)
            {
                throw ex;
                Log("Server Side Exception Caught!");
            }
        }

        public void GetValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out byte[] bitmapBytes)
        {
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = null;
            lName = null;
            bitmapBytes = null;

            uint accountNumber;
            uint pinNumber;
            int currentBalance;
            string firstName;
            string lastName;
            byte[] tempImage;

            int numEntries = foob.GetNumEntries();
            for (int i = 0;  i < numEntries; i++)
            {
                

                try
                {
                    foob.GetValuesForEntry(i, out accountNumber, out pinNumber, out currentBalance, out firstName, out lastName, out tempImage);
                    if (lastName.ToLower().Contains(searchText.ToLower()))
                    {
                        acctNo = accountNumber;
                        pin = pinNumber;
                        bal = currentBalance;
                        fName = firstName;
                        lName = lastName;
                        bitmapBytes = tempImage;
                        Log("Found the matching search value! " + fName + " : " + lName);
                        
                        break;
                    }
                    Console.WriteLine(i);
                } catch (FaultException<ServerFailureException> ex) { }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Log(string logString)
        {
            LogNumber++;
            Console.WriteLine(LogNumber + " : " + logString + " @ " + DateTime.Now.ToShortTimeString());
        }
    }
}
