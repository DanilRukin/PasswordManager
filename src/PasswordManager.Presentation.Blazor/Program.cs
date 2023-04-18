using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using PasswordManager.Application;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Data;
using PasswordManager.Presentation.Blazor;
using PasswordManager.Presentation.Blazor.Services.Mappers;
using PasswordManager.Presentation.Blazor.ViewModels;
using System.Reflection;

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

builder.Services.AddApplicationServices();
builder.Services.AddMediatR(typeof(Marker).GetTypeInfo().Assembly);
builder.Services.AddScoped<IMapper<PasswordOptionsViewModel, PasswordOptionsDto>, PasswordOptionsViewModelToPasswordOptionsDtoMapper>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

await builder.Build().RunAsync();
