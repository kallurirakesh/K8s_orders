using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
using Microsoft.Identity.Web;
using Products.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var endpoint =
        configuration["CosmosDb:AccountEndpoint"];

    var key =
        configuration["CosmosDb:AccountKey"];

    return new CosmosClient(endpoint, key);
});
builder.Services.AddScoped<ILoanRepository,
    CosmosLoanRepository>();

// Register ProductsService which will manage MongoDB access
builder.Services.AddSingleton<ProductsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
