using Microsoft.AspNetCore.Authentication.Cookies;
using ProveeduriaGanadera.Application.Common;
using ProveeduriaGanadera.Infrastructure;
using ProveeduriaGanadera.UI.Services;
using ProveeduriaGanadera.Web.Components;
using ProveeduriaGanadera.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IArchivoAlmacenamientoService, ArchivoAlmacenamientoWebService>();
builder.Services.AddScoped<CarritoNotificador>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/cuenta/ingresar";
        options.AccessDeniedPath = "/cuenta/ingresar";
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(ProveeduriaGanadera.UI.Pages.Categorias).Assembly);

app.Run();
