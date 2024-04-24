namespace EasyGames.Models
{
    public class TransactionDTO
    {
        public int TransactionID { get; set; }
        public int TransactionTypeID { get; set; }

        public int ClientID { get; set; }

        public decimal Amount { get; set; }

        public string? Comment { get; set; }

        public bool? Delete { get; set; }
    }
}
