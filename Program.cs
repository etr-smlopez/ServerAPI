using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServerAPI.Cache;
using ServerAPI.Hubs;
using ServerAPI.SQLAccess;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
 
builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<DataCache>();  
builder.Services.AddScoped<DataHub>();
builder.Services.AddControllers();
builder.Services.AddSignalR();
 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.CommandTimeout(60);  
        })
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
      .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted }))
  .BuildServiceProvider();;


builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(120); // Set the desired timeout value
    options.KeepAliveInterval = TimeSpan.FromSeconds(120);
});
  

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


    app.UseDeveloperExceptionPage();
    app.UseDatabaseErrorPage();
 
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<DataHub>("/datahub");
});
 

app.Run();
 