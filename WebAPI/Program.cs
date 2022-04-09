using JAModel;

using JAConsoleDL;
using JAConsoleBL;


var builder = WebApplication.CreateBuilder(args);

//dotnet run --configuration Debug
// Add services to the container.



builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering dependencies in Services container to be dep injected
//3 different lifecycles/how often they get recreated
//Scoped, Transient, and Singleton
//Singleton - for the entire application's lifetime, it shares the one instance
//Scoped - For every HTTP request, the new instance is spun up
//Transient - for every time it calls for the class, it spins up a new intance

builder.Services.AddScoped<IRepo>(ctx => new DBRespository(builder.Configuration.GetConnectionString("SLDB")));
builder.Services.AddScoped<IJABL, ConsoleProjBL>();


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
