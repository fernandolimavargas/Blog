using BlogFecomercio.Context;
using BlogFecomercio.Services.Implementations;
using BlogFecomercio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string sqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new Exception("A string de conexão não foi configurada");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(sqlServerConnection));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ICurtidaService, CurtidaService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();


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
