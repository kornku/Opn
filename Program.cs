using Opn.Infrastructures;
using Opn.Interfaces;
using Opn.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(p =>
    p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<IRepository,UserRepository>();
builder.Services.AddTransient<IRegisterService, RegisterService>();
builder.Services.AddTransient<IUserService, ProfileService>();

var app = builder.Build();

app.UseCors("corsapp");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();

app.Run();