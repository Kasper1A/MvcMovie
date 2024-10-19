using MvcMovie.Controllers;
using MvcMovie.Services;

var builder = WebApplication.CreateBuilder(args);

// Lägg till MVC-tjänster
builder.Services.AddControllersWithViews();

// Konfigurera HttpClient som en tjänst
builder.Services.AddHttpClient<HomeController>();
// Registrera MovieService och HttpClient
builder.Services.AddHttpClient<MovieService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5296/"); // Ange basadressen för API:et
});

var app = builder.Build();

// Din övriga konfiguration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Schema}/{id?}");

app.Run();
