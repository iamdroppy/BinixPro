using BinixPro.Database;
using BinixPro.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // ASP.NET Core API services
    services.AddEndpointsApiExplorer().AddSwaggerGen().AddControllers();
    services.AddBinixDb("binix.db");
}

void Configure(WebApplication app)
{
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
}

var configuration = builder.Configuration;
ConfigureServices(builder.Services, configuration);
Configure(builder.Build());
