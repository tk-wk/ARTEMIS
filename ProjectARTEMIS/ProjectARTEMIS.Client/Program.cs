using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7101/")
});

await builder.Build()
    .RunAsync();

// ---------------- GREEN THEME ----------------------- //
var greenWhiteTheme = new MudTheme
{
    PaletteLight = new PaletteLight
    {
        // Primary green
        Primary = "#2E7D32",
        PrimaryDarken = "#1B5E20",
        PrimaryLighten = "#4CAF50",
        PrimaryContrastText = "#FFFFFF",

        // White surfaces
        Background = "#F5F5F5",
        Surface = "#FFFFFF",
        DrawerBackground = "#FFFFFF",
        DrawerText = "#2E7D32",

        // Appbar in green
        AppbarBackground = "#2E7D32",
        AppbarText = "#FFFFFF",

        // Text
        TextPrimary = "#212121",
        TextSecondary = "#555555",
    }
};