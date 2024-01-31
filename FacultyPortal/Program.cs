using FacultyPortal.Authentication;
using FacultyPortal.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddSingleton<IRegistrationService, RegistrationService>();
builder.Services.AddSingleton<IProfile, ProfileService>();
builder.Services.AddSingleton<IEmailExistChecker, EmailExistChecker>();
builder.Services.AddSingleton<IForgetPassword, ForgetPassword>();


builder.Services.AddHttpClient<IForgetPassword, ForgetPassword>(e =>
{
	e.BaseAddress = new Uri("http://localhost:5011/");
});
builder.Services.AddHttpClient<IRegistrationService, RegistrationService>(e=>
{
    e.BaseAddress = new Uri("http://localhost:5011/");
});
builder.Services.AddHttpClient<IEmailExistChecker, EmailExistChecker>(e =>
{
	e.BaseAddress = new Uri("http://localhost:5011/");
});
builder.Services.AddHttpClient<ILoginService, LoginService>(e=>
{
    e.BaseAddress = new Uri("http://localhost:5011/");

});

builder.Services.AddHttpClient<IProfile, ProfileService>(e =>
{
    e.BaseAddress = new Uri("http://localhost:5011/");

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
