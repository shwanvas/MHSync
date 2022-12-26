using ApacheKafkaUpdateService;
using ApacheKafkaUpdateService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<ApacheKafkaUpdate>();
builder.Services.AddDbContext<SyncDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SyncDbConnection"))); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();
// Configure the HTTP request pipeline.
app.Run();
