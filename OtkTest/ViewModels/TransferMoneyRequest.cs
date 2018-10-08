namespace OtkTest.ViewModels
{
    public class TransferMoneyRequest
    {
        public long SenderAccountId { get; set; }
        public long RecepientAccountId { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
