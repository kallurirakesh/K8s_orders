using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.API
{
    public class CosmosLoanRepository : ILoanRepository
    {
        private readonly Container  _container;

        public CosmosLoanRepository(CosmosClient client,IOptions<CosmosDbOptions> options)
        {
            var settings = options.Value;
            _container = client.GetDatabase(settings.DatabaseName).GetContainer(settings.ContainerName);
        }
    
        public async Task<LoanApplication> CreateAsync(LoanApplication loan)
        {
           var response = await _container.CreateItemAsync(loan, new PartitionKey(loan.CustomerId));    
           return response.Resource;
        }

        

        public async Task<(List<LoanApplication>, string? ContinuationToken)> GetByCustomerAsync(string customerId,string ? continuationToken)
        {
            var query =
        new QueryDefinition(
            "SELECT * FROM c " +
            "WHERE c.customerId = @customerId")
        .WithParameter("@customerId", customerId);

            var iterator =
        _container.GetItemQueryIterator<LoanApplication>(
            query,
            continuationToken,
            new QueryRequestOptions
            {
                PartitionKey =
                    new PartitionKey(customerId),
                MaxItemCount = 2
            });

            var response =
        await iterator.ReadNextAsync();

            return (
                response.ToList(),
                response.ContinuationToken
            );
        }
    }
}
