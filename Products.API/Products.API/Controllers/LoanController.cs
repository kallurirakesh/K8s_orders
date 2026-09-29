using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository loanRepository;
        public LoanController(ILoanRepository loanRepository)
        {
            this.loanRepository = loanRepository;
        }
        // POST: api/Loan   
        [HttpPost]
        public async Task<IActionResult> CreateLoanApplication([FromBody] LoanApplication loanApplication)
        {
            if (loanApplication == null)
            {
                return BadRequest("Loan application is null.");
            }
            var createdLoan = await loanRepository.CreateAsync(loanApplication);
            return Ok(createdLoan);
        }
        // GetByCustomerAsync 
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetLoanApplicationsByCustomer(string customerId, [FromQuery] string? continuationToken)
        {
            var (loans, token) = await loanRepository.GetByCustomerAsync(customerId, continuationToken);
          //  Response.Headers.Add("X-Continuation-Token", token ?? string.Empty);
            return Ok(new
            {
                loans,
                continuationToken = token
            });
        }
    }
}
