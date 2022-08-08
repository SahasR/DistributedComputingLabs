using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial_1
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
            for (int i = 0; i < 1000; i++)
            {
                newGenerator.GetNextAccount(out pin,out acctNo,out firstName,out lastName,out balance);
                DataStruct currAccount = new DataStruct(acctNo, pin, balance, firstName, lastName);
                dataStruct.Add(currAccount);
            }
        }
    }
}
