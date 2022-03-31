using Applicaiton.DAOInterfaces;
using Applicaiton.ServiceImpl;
using Contracts;
using JsonDataAccess.DAOImpl;
using JsonDataAccess.JsonContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//services login
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ISubForumService, SubForumServiceImpl>();
//file 
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IForumDAO, ForumDAOImpl>();

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