﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    internal class DatabaseGenerator
    {
        private string GetFirstName(int seed)
        {
            Random random = new Random(seed);
            string[] firstNames = { "Sahas", "Abinaya", "Venumi", "Kapila", "Anurudda", "Pathum" };
            int randomNumber = random.Next(0, 6);
            string firstName = firstNames[randomNumber];
            return firstName;
        }

        private string GetLastName(int seed)
        {
            Random random = new Random(seed);
            string[] lastNames = { "Gunasekara", "Sritharan", "Kaluarachchi", "Kaluarachchi", "Padeniya", "Nissanka" };
            int randomNumber = random.Next(0, 6);
            string lastName = lastNames[randomNumber];
            return lastName;
        }

        private uint GetPIN(int seed)
        {
            Random random = new Random(seed);
            uint pin = (uint)random.Next(0, 10000);
            return pin;
        }

        private uint GetAcctNo(int seed)
        {
            Random random = new Random(seed);
            uint accNum = (uint)random.Next(0, 10000000);
            return accNum;
        }

        private int GetBalance(int seed)
        {
            Random random = new Random(seed);
            int balance = random.Next(0, 1000000000);
            return balance;
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance, int seed)
        {
            pin = GetPIN(seed);
            acctNo = GetAcctNo(seed);
            firstName = GetFirstName(seed);
            lastName = GetLastName(seed);
            balance = GetBalance(seed);
        }
    }
}
