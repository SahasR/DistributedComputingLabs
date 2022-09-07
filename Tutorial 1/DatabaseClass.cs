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
            //Will be storing image as a location url, if you store as bitmap your application will be very heavy to load 100s of BitMaps in one area
            //Will throw a OutOfMemory Exception if you try to create and store the Bitmaps, so we only create at the point of request
            DatabaseGenerator newGenerator = new DatabaseGenerator();
            for (int i = 0; i < 500; i++)
            {
                newGenerator.GetNextAccount(out pin, out acctNo, out firstName, out lastName, out balance, out image, i);
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
