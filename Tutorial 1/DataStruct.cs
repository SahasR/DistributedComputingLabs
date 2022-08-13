using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    internal class DataStruct
    {
        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;
        public string image;

        public DataStruct()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
            image = "";
        }

        public DataStruct(uint acctNo, uint pin, int balance, string firstName, string lastName, string image)
        {
            this.acctNo = acctNo;
            this.pin = pin;
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
            this.image = image;
        }
    }
}
