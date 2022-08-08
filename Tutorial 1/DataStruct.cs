using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial_1
{
    internal class DataStruct
    {
        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;

        public DataStruct()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
        }

        public DataStruct(uint acctNo, uint pin, int balance, string firstName, string lastName)
        {
            this.acctNo = acctNo;
            this.pin = pin;
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}
