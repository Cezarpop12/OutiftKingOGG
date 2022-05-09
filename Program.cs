using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Configuration.GetConnectionString("OutfitKingDB");
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

/// <summary>
/// Hoe moet de applicatie reageren op een request? : pipeline
/// </summary>

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/// <summary>
/// Als je nog niet in de url heb gespecificeerd wat je wilt zien (dus alleen https:/localhost), dan zal deze pagina getoond worden.
/// Deze pagina is {controller=Home}/{action=Index}/{id?}
/// </summary>

app.Run();
