namespace OtkTest.ViewModels
{
    public class Account
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public int AccountType { get; set; }

        public string AccountTypeName { get; set; }

        public decimal Money { get; set; }

        public int Currency { get; set; }

        public int BankId { get; set; }
    }
}
