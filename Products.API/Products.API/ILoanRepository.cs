namespace Products.API
{
    public interface ILoanRepository
    {
        Task<LoanApplication> CreateAsync(
      LoanApplication loan);

        Task<(List<LoanApplication>, string? ContinuationToken)> GetByCustomerAsync(
            string customerId, string? continuationToken);
    }
}
