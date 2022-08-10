using System;
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
            DatabaseGenerator newGenerator = new DatabaseGenerator();
            Random rand = new Random(1);
            int numElements = rand.Next(0, 1000);
            for (int i = 0; i < numElements; i++)
            {
                newGenerator.GetNextAccount(out pin, out acctNo, out firstName, out lastName, out balance, i);
                DataStruct currAccount = new DataStruct(acctNo, pin, balance, firstName, lastName);
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

        public int GetNumRecords()
        {
            return dataStruct.Count;
        }
    }
}
