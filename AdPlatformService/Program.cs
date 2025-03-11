var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AdPlatformService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
