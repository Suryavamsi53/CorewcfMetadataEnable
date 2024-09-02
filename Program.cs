using CoreWCFService1.DataAccessLayer;
using CoreWCFService1.IServices;
using CoreWCFService1.Models;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel;
using CoreWCF.Description;

var builder = WebApplication.CreateBuilder();

// Add services to the container
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

// Register services with the required connection string
builder.Services.AddSingleton<ILookupService>(new LookupService("Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True"));
builder.Services.AddSingleton<IAccountHolderService>(new AccountHolderService("Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True"));
builder.Services.AddSingleton<ITransactionService>(new TransactionService("Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True"));
builder.Services.AddSingleton<IAccountService>(new AccountService("Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True"));
builder.Services.AddSingleton<IAddressService>(new AddressService("Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True"));

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    // Configure LookupService
    serviceBuilder.AddService<LookupService>();
    serviceBuilder.AddServiceEndpoint<LookupService, ILookupService>(
        new BasicHttpBinding(BasicHttpSecurityMode.Transport),
        "/LookupService.svc"
    );

    // Configure AccountHolderService
    serviceBuilder.AddService<AccountHolderService>();
    serviceBuilder.AddServiceEndpoint<AccountHolderService, IAccountHolderService>(
        new BasicHttpBinding(BasicHttpSecurityMode.Transport),
        "/AccountHolderService.svc"
    );

    // Configure TransactionService
    serviceBuilder.AddService<TransactionService>();
    serviceBuilder.AddServiceEndpoint<TransactionService, ITransactionService>(
        new BasicHttpBinding(BasicHttpSecurityMode.Transport),
        "/TransactionService.svc"
    );

    // Configure AccountService
    serviceBuilder.AddService<AccountService>();
    serviceBuilder.AddServiceEndpoint<AccountService, IAccountService>(
        new BasicHttpBinding(BasicHttpSecurityMode.Transport),
        "/AccountService.svc"
    );

    // Configure AddressService
    serviceBuilder.AddService<AddressService>();
    serviceBuilder.AddServiceEndpoint<AddressService, IAddressService>(
        new BasicHttpBinding(BasicHttpSecurityMode.Transport),
        "/AddressService.svc"
    );

    // Enable metadata for all services
    ConfigureServiceMetadata<LookupService>(serviceBuilder);
    ConfigureServiceMetadata<AccountHolderService>(serviceBuilder);
    ConfigureServiceMetadata<TransactionService>(serviceBuilder);
    ConfigureServiceMetadata<AccountService>(serviceBuilder);
    ConfigureServiceMetadata<AddressService>(serviceBuilder);
});

app.Run();

// Helper method to configure metadata
static void ConfigureServiceMetadata<TService>(IServiceBuilder serviceBuilder)
    where TService : class
{
    serviceBuilder.ConfigureServiceHostBase<TService>(serviceHost =>
    {
        var serviceMetadataBehavior = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
        if (serviceMetadataBehavior != null)
        {
            serviceMetadataBehavior.HttpsGetEnabled = true;
            serviceMetadataBehavior.HttpGetEnabled = true; // Optional
        }
    });
}
