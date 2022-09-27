namespace WebGUI.Models
{
    public class StudentTable
    {
        public StudentTable(int id, string firstName, string lastName, int balance, int acct, int pin, string bitmapImage)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.balance = balance;
            this.acctNo = acct;
            this.pin = pin;
            this.image = bitmapImage;
        }

        public StudentTable()
        {

        }

        public int id { get; set; }
        public int acctNo { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int pin { get; set; }
        public int balance { get; set; }
        public string image { get; set; }
    }
}
