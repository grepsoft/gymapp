using gymappyt.Components;
using gymappyt.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(option => 
    option.UseMySQL(connectionString)
);

builder.Services.AddScoped<gymappyt.Services.UserService>();
builder.Services.AddScoped<gymappyt.Services.AuthService>();
builder.Services.AddScoped<gymappyt.Services.InstructorService>();
builder.Services.AddScoped<gymappyt.Services.BookingService>();
builder.Services.AddScoped<gymappyt.Services.GymClassService>();
builder.Services.AddScoped<gymappyt.Services.FakePaymentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
