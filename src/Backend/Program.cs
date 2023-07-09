using Backend.Silo.Counters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// orleans
builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder
        .UseDashboard( options => 
        {
            options.HideTrace = true;
            options.HostSelf = true;
        })        .UseLocalhostClustering()
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

app.Map("/dashboard", x => x.UseOrleansDashboard());

app.MapControllers();

app.Run();