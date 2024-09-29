var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthorization();  

var app = builder.Build();



app.UseAuthorization();  

app.MapControllers();

app.Run();
