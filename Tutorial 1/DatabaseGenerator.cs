using System;
using System.Collections.Generic;
using System.Drawing;
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
            string[] firstNames = { "Sahas", "Abinaya", "Venumi", "Kapila", "Anurudda", "Pathum", "Nalaka", "Renuja", "Nisal"};
            int randomNumber = random.Next(0, 9);
            string firstName = firstNames[randomNumber];
            return firstName;
        }

        private string GetLastName(int seed)
        {
            Random random = new Random(seed);
            string[] lastNames = { "Gunasekara", "Sritharan", "Kaluarachchi", "Kaluarachchi", "Padeniya", "Nissanka", "Godahewa", "Rajapakse", "Seneviratne"};
            int randomNumber = random.Next(0, 9);
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

        private string GetImage(int seed)
        {
            string[] locations = { "C:/Users/sahas/source/repos/DistributedComputingLab01/Tutorial 1/Images/imageedit_3_5955217527.jpg", 
                "C:/Users/sahas/source/repos/DistributedComputingLab01/Tutorial 1/Images/rsz_1dalle_2022-08-03_225034_-_aladin_driving_a_black_car_van_gogh_styke.png" };

            Random random = new Random(seed);
            string location = locations[random.Next(0,2)];
            return location;
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance, out string image ,int seed)
        {
            pin = GetPIN(seed);
            acctNo = GetAcctNo(seed);
            firstName = GetFirstName(seed);
            lastName = GetLastName(seed);
            balance = GetBalance(seed);
            image = GetImage(seed);
        }
    }
}
