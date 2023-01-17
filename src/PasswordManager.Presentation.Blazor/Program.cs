using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PasswordManager.Data;
using PasswordManager.Presentation.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var config = builder.Configuration;
string migrationAssembly = "PasswordManager.Data";

//builder.Services.AddDbContext<PasswordManagerDbContext>(optionBuilder =>
//{
//    optionBuilder.UseSqlite(config.GetConnectionString("SqLite"),
//        sql => sql.MigrationsAssembly(migrationAssembly));
//});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
