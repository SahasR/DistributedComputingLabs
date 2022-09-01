using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClasses
{
    public class DataIntermed
    {
        public int bal;
        public uint acct;
        public uint pin;
        public string fname;
        public string lname;
        public byte[] image;

        public DataIntermed(int bal, uint acct, uint pin, string fname, string lname, byte[] image)
        {
            this.bal = bal;
            this.acct = acct;
            this.pin = pin;
            this.fname = fname;
            this.lname = lname;
            this.image = image;
        }
    }
}
