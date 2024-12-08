var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("StudentApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5170/");
});

builder.Services.AddScoped<Students_Services>();
builder.Services.AddScoped<Drivers_Services>();
builder.Services.AddScoped<Ways_Services>();
builder.Services.AddScoped<Pays_Services>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}"); 
app.Run();

