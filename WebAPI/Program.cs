using Applicaiton.ServiceImpl;
using Application.DAOInterfaces;
using Contracts;
using EFCDataAccess;
using EFCDataAccess.DAOImpl;
using Microsoft.OpenApi.Models;



using (DBContext ctx = new())
{
    ctx.Seed();
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//services login
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ISubForumService, SubForumServiceImpl>();
builder.Services.AddScoped<IPostSerivce, PostServiceImpl>();
builder.Services.AddScoped<ICommentService, CommentServiceImpl>();
builder.Services.AddScoped<IVoteService, VoteServiceImpl>();

//db 
//
builder.Services.AddScoped<ISubForumDAO, SubForumDAOImpl>();
builder.Services.AddScoped<ICommentDAO, CommentDAOImpl>();
builder.Services.AddScoped<IPostDAO, PostDAOImpl>();
builder.Services.AddScoped<IUserDAO, UserDAOImpl>();
builder.Services.AddScoped<IVoteDAO, VoteDAOImpl>();

builder.Services.AddDbContext<DBContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();