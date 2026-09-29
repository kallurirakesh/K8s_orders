namespace Products.API
{
    public class LoanApplication
    {
        public string Id { get; set; } = default!;

        public string CustomerId { get; set; } = default!;

        public string LoanType { get; set; } = default!;

        public decimal Amount { get; set; }

        public string Status { get; set; } = default!;

        public DateTime CreatedDate { get; set; }
    }
}
