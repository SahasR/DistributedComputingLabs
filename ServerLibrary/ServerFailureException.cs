using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BankServer
{
    [DataContract]
    public class ServerFailureException
    {
        private string operation;
        private string problemtype;

        [DataMember]
        public string Operation {
            get { return operation; }
            set { operation = value; }
        }

        [DataMember]
        public string ProblemType
        {
            get{ return problemtype; }
            set { problemtype = value; }
        }
    }
}
