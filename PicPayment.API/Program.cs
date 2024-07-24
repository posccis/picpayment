using Microsoft.EntityFrameworkCore;
using PicPayment.Application.Interfaces;
using PicPayment.Application.Services;
using PicPayment.Domain.Domains;
using PicPayment.Persistence;
using PicPayment.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PicPaymentContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("PicPayment.API"));
});

builder.Services.AddScoped<IUsuarioService<Usuario>, UsuarioService>();

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

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<PicPaymentContext>();
    context.Database.Migrate();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    throw;
    //var logger = services.GetRequiredService<ILogger<Program>>();
    //logger.LogError(ex, "Ocorreu um erro durante a migration.");
}

app.Run();
