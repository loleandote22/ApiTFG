using ApiTFG;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MiConexionSql")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => 
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

//builder.WebHost.UseKestrel();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.MapGet("", () => Results.Ok(new { mensaje = "API funcionando correctamente" }));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
