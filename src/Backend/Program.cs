using Shared.Config;

var builder = WebApplication.CreateBuilder(args);

int gatewayPort = 30000;
int siloPort = 11111;
int dashboardPort = 8080;

if (args.Length > 0)
{
    int delta = int.Parse(args[1]);
    gatewayPort += delta;
    siloPort += delta;
    dashboardPort += delta;
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// orleans
builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder
        .UseDashboard(options =>
        {
            options.HideTrace = true;
            options.Port = dashboardPort;
        }).UseLocalhostClustering
        (
            gatewayPort: gatewayPort, 
            siloPort: siloPort
        )
        .ConfigureLogging(logging => logging.AddConsole());
}).UseConsoleLifetime();

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