using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureSecretsManager;
using AzureSecretsManager.Models;
using AzureStorageWrapper;
using AzureStorageWrapper.Models;
using AzureStorageWrapper.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
SecretManager.ConfigureSecrets(builder, "weerly-app-settings");
builder.Services.Configure<AzureStorageKeys>(builder.Configuration.GetSection("AzureStorage"));
builder.Services.Configure<AzureSqlKeys>(builder.Configuration.GetSection("AzureSql"));
builder.Services.AddScoped<IAzureStorageHandler, AzureStorageHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();