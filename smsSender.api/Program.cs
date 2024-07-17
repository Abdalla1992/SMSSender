
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using smsSender.Data.Infrastructure.Context;
using System.Reflection;
using smsSender.Core;
using smsSender.Data.Infrastructure;
using smsSender.Data.Infrastructure.Dto.SettingsDto;
using smsSender.Api.Middleware;
using smsSender.Api.Infrastructure.Hosting;



var configuration = GetConfiguration();
    Log.Logger = CreateSerilogLogger(configuration);

    try
    {
        Log.Information("Configuring web host ({ApplicationContext})...");
        var builder = WebApplication.CreateBuilder(args);
        #region DI

        builder.Services.AddRepository();
        builder.Services.AddAppService();

        #endregion

        #region DbContext & Settings
        AddCustomDbContext(builder.Services);
        AppSettingsConfig(builder.Services);
        #endregion

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.ConfigureExceptionHandler(Log.Logger);
    //app.UseAuthorization();

    app.MapControllers();

    #region Migration
    Log.Information("Applying migrations ({ApplicationContext})...");
    app.MigrateDbContext<AppDbContext>((context, services) =>
    {
        context.Database.Migrate();
    });

    Log.Information("Starting web host ({ApplicationContext})...");

    #endregion

    app.Run();


        return 0;
    }
    catch (Exception ex)
    {

        Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!");
        return 1;
    }
    finally
    {
        Log.CloseAndFlush();
    }

    Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {

        var logPath = Path.Combine(configuration["Serilog:LogPath"], ".log");
        int.TryParse(configuration["Serilog:FileLogLevel"], out int fileLogLevel);
        int.TryParse(configuration["Serilog:RollingInterval"], out int rollingInterval);

        return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProperty("ApplicationContext", "smsSender.Api")
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Verbose)
            .ReadFrom.Configuration(configuration)
            .WriteTo.File(logPath, (LogEventLevel)fileLogLevel, rollingInterval: (RollingInterval)rollingInterval)

            .CreateLogger();
    }

    IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    IServiceCollection AddCustomDbContext(IServiceCollection services)
    {
        string connString = configuration["ConnectionString"];
        string envConnString = Environment.GetEnvironmentVariable("ConnectionStrings__DBConString");
        if (!string.IsNullOrWhiteSpace(envConnString))
        {
            connString = envConnString;
        }
        Log.Information($"Api will run with connection string: {connString}");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
        },
        ServiceLifetime.Scoped
        );


        return services;
    }

    IServiceCollection AppSettingsConfig(IServiceCollection services)
    {
    TwilioSettings setting = new();
    configuration.Bind("TwilioSettings", setting);
    services.AddSingleton(setting);

    NexmoSettings nexmoSettings = new();
    configuration.Bind("NexmoSettings", nexmoSettings);
    services.AddSingleton(nexmoSettings);

    return services;
    }
