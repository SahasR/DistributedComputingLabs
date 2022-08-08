using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    internal class DatabaseGenerator
    {
        private string GetFirstName()
        {
            Random random = new Random();
            string[] firstNames = { "Sahas", "Abinaya", "Venumi", "Kapila", "Anurudda", "Pathum" };
            int randomNumber = random.Next(0, 6);
            string firstName = firstNames[randomNumber];
            return firstName;
        }

        private string GetLastName()
        {
            Random random = new Random();
            string[] lastNames = { "Gunasekara", "Sritharan", "Kaluarachchi", "Kaluarachchi", "Padeniya", "Nissanka" };
            int randomNumber = random.Next(0, 6);
            string lastName = lastNames[randomNumber];
            return lastName;
        }

        private uint GetPIN()
        {
            Random random = new Random();
            uint pin = (uint)random.Next(0, 10000);
            return pin;
        }

        private uint GetAcctNo()
        {
            Random random = new Random();
            uint accNum = (uint)random.Next(0, 10000000);
            return accNum;
        }

        private int GetBalance()
        {
            Random random = new Random();
            int balance = random.Next(0, 1000000000);
            return balance;
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstName();
            lastName = GetLastName();
            balance = GetBalance();
        }
    }
}
