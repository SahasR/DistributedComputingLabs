using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib;

namespace BankServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class BankServerImplementation : BankServerInterface
    {
        private DatabaseClass database;
        public BankServerImplementation()
        {
            database = new DatabaseClass();
        }
        public int GetNumEntries()
        {
            return database.GetNumRecords();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            acctNo = database.GetAcctNoByIndex(index);
            pin = database.GetPINByIndex(index);
            bal = database.GetBalanceByIndex(index);
            fName = database.GetFirstNameByIndex(index);
            lName = database.GetLastNameByIndex(index);
        }

    }
}
