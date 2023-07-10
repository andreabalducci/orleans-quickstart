using Standalone.Silo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseOrleans(siloBuilder=>
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.UseDashboard();

        siloBuilder.ConfigureServices(s =>
        {
            s.AddGrainService<DataService>()
                .AddSingleton<IDataServiceClient, DataServiceClient>();
        });

    })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();

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