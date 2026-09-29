namespace Products.API
{
    public class CosmosDbOptions
    {
        public string AccountEndpoint { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public string ContainerName { get; set; } = string.Empty;
    }
}
