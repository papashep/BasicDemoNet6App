using BasicDemoNet6.Data;
using BasicDemoNet6.Options;

using Microsoft.AspNetCore;

var builder = WebApplication.CreateBuilder( args );

var memCollection = new Dictionary<string, string>
{
    // Commented out while testing other sections such as Azure KeyVault
    // { "MainSetting:SubSetting","sub setting from In-Memory collection" }
};


// Question 1 - I cannot get the following line to work also see Question 3 below ?????????
// IHostEnvironment env = hostingContext.HostingEnvironment;

// Question 2 - I cannot get the following line to work ?????????
// builder.Configuration.Sources.Clear();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.Configure<EmailSettingsOptions>( builder.Configuration.GetSection( "EmailSettings" ) );


builder.Configuration.AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true );

// Question 3 - I cannot get the following line to work, see Question 1 ?????????
// builder.Configuration?.AddJsonFile( $"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true );


builder.Configuration.AddJsonFile( "custom.json", optional: true, reloadOnChange: true );

builder.Configuration.AddXmlFile( "custom.xml", optional: true, reloadOnChange: true );

builder.Configuration.AddIniFile( "custom.ini", optional: true, reloadOnChange: true );

builder.Configuration.AddInMemoryCollection(memCollection );


// Question 4 - I cannot get the following section on Azure KeyVault to work ?????????
//if ( hostingContext.HostingEnvironment.IsProduction() )
//{
//    var builtConfig = builder.Build();

//    var azureServiceTokenProvider = new AzureServiceTokenProvider();
//    var keyVaultClient = new KeyVaultClient(
//        new KeyVaultClient.AuthenticationCallback(
//            azureServiceTokenProvider.KeyVaultTokenCallback ) );

//    builder.AddAzureKeyVault(
//        $"https://{builtConfig[ "KeyVaultName" ]}.vault.azure.net/",
//        keyVaultClient,
//        new DefaultKeyVaultSecretManager() );
//}

// Question 5 - Do I need the following two lines of code ?????????
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

var app = builder.Build();

if ( app.Environment.IsDevelopment() )
{
    builder.Configuration.AddUserSecrets<Program>();
}



// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment() )
{
    app.UseExceptionHandler( "/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage( "/_Host" );

app.Run();
