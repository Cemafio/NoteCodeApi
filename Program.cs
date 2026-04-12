using Microsoft.EntityFrameworkCore;
using NoteCodeApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//COnnecte with PostgreSQL
builder.Services.AddDbContext<NoteCodeDb>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("baseConnection")  
    )
);

//Add controller
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Activation controller
app.MapControllers();

app.Run();