using LaundryBooking.Domain.Interfaces;
using LaundryBooking.Infrastructure.Repositories;
using LaundryBooking.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LaundryBooking.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Hämta connection strängen
        var assembly = typeof(MauiProgram).Assembly;
        using var stream = assembly.GetManifestResourceStream("LaundryBooking.Maui.appsettings.json"); // öppnar appsettings.json filen där connection strängen finns
        var config = new ConfigurationBuilder().AddJsonStream(stream!).Build();
        var connectionString = config["MongoDb:ConnectionString"]!;

        // Registrera repositories
        builder.Services.AddSingleton<IBookingRepository>(new MongoBookingRepository(connectionString)); // AddSingleton betyder att den skapar en instans och använder den konstant sedan
        builder.Services.AddSingleton<IIssueRepository>(new MongoIssueRepository(connectionString));
        builder.Services.AddSingleton<IHousingCooperativeRepository>(new MongoHousingCooperativeRepository(connectionString));

        // Registrera services
        builder.Services.AddSingleton<BookingService>(); // MAUI skapar BookingService automatiskt och injicerar IBookingRepository som redan är registrerad ovan.
        builder.Services.AddSingleton<IssueService>();
        builder.Services.AddSingleton<HousingCooperativeService>();
        builder.Services.AddSingleton<SessionService>(); // Även här AddSingleton för att använda samma session genom alla olika pages

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}