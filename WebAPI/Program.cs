using Applicaiton.DAOInterfaces;
using Applicaiton.ServiceImpl;
using Contracts;
using EFCDataAccess;
using EFCDataAccess.DAOImpl;


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
//db 
builder.Services.AddScoped<IForumDAO, ForumDAOImpl>();
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