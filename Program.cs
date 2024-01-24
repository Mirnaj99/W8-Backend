using Microsoft.EntityFrameworkCore;
using W8_Backend.Data;
using W8_Backend.Services;
using W8_Backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>
           (options => options.UseSqlServer(builder.Configuration.GetConnectionString("W8")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInitDB, InitDB>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<ILogsService, LogsService>();
builder.Services.AddScoped<ISystemVariablesService, SystemVariablesService>();
builder.Services.AddScoped<ILogsService, LogsService>();
builder.Services.AddScoped<IFilesService, FilesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}



//app.UseHttpsRedirection();

app.UseCors(x => x
              .SetIsOriginAllowed(origin => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());

app.UseAuthorization();

app.MapControllers();


app.Run();
