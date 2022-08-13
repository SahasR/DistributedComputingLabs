using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    public class DatabaseClass
    {
        List<DataStruct> dataStruct;

        public DatabaseClass()
        {
            dataStruct = new List<DataStruct>();
            uint pin;
            uint acctNo;
            string firstName;
            string lastName;
            int balance;
            string image;

            DatabaseGenerator newGenerator = new DatabaseGenerator();
            Random rand = new Random(1);
            int numElements = rand.Next(0, 1000);
            for (int i = 0; i < numElements; i++)
            {
                newGenerator.GetNextAccount(out pin, out acctNo, out firstName, out lastName, out balance, out image ,i);
                DataStruct currAccount = new DataStruct(acctNo, pin, balance, firstName, lastName, image);
                dataStruct.Add(currAccount);
            }
        }

        public uint GetAcctNoByIndex(int index)
        {
            DataStruct returnAccount = dataStruct[index];
            return returnAccount.acctNo;
        }

        public uint GetPINByIndex(int index)
        {
            DataStruct returnAccount = dataStruct[index];
            return returnAccount.pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            DataStruct returnAccount = dataStruct[index];
            return returnAccount.firstName;
        }

        public string GetLastNameByIndex(int index)
        {
            DataStruct returnAccount = dataStruct[index];
            return returnAccount.lastName;
        }

        public int GetBalanceByIndex(int index)
        {
            DataStruct returnAccount = dataStruct[index];
            return returnAccount.balance;
        }

        public Bitmap GetImage(int index)
        {
            DataStruct returnAccount = dataStruct[index];
            Bitmap image = new Bitmap(returnAccount.image, true);
            return image;
        }

        public int GetNumRecords()
        {
            return dataStruct.Count;
        }
    }
}
