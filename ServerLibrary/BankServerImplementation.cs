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
    public class BankServerImplementation : BankServerInterface
    {
        DatabaseClass database;
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
            if (index >= 0 && index < database.GetNumRecords())
            {
                acctNo = database.GetAcctNoByIndex(index);
                pin = database.GetPINByIndex(index);
                bal = database.GetBalanceByIndex(index);
                fName = database.GetFirstNameByIndex(index);
                lName = database.GetLastNameByIndex(index);
            } else
            {
                ServerFailureException sf = new ServerFailureException();
                sf.Operation = "You tried to do something you're not allowed.";
                sf.ProblemType = "Out of index values";
                throw new FaultException<ServerFailureException>(sf);
            }
            
            
        }

    }
}
