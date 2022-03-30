using Applicaiton.DAOInterfaces;
using Applicaiton.ServiceImpl;
using BlazorAppForum.Authentication;
using Contracts;
using JsonDataAccess.DAOImpl;
using JsonDataAccess.JsonContext;
using Microsoft.AspNetCore.Components.Authorization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//add scopes
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();
//services login
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ISubForumService, SubForumServiceImpl>();
//file 
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IForumDAO, ForumDAOImpl>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        pb =>
            pb.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));

    options.AddPolicy("User",
        a => 
            a.RequireAuthenticatedUser().RequireClaim("Role", "User"));
    
    options.AddPolicy("SubForumAdmin",
        a => 
            a.RequireAuthenticatedUser().RequireClaim("Role", "SubForumAdmin","Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();