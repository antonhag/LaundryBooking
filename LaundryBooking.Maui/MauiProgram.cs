using LaundryBooking.Application.Facade;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Services;
using LaundryBooking.Domain.Interfaces;
using LaundryBooking.Infrastructure.Data;
using LaundryBooking.Infrastructure.Repositories;
using LaundryBooking.Maui.ViewModels;
using LaundryBooking.Maui.Views;
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
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        // Registrera MongoDbContext
        builder.Services.AddSingleton(new MongoDbContext(connectionString));

        // Registrera repositories
        builder.Services.AddSingleton<IBookingRepository, MongoBookingRepository>(); // AddSingleton betyder att den skapar en instans och använder den konstant sedan
        builder.Services.AddSingleton<IIssueRepository, MongoIssueRepository>();
        builder.Services.AddSingleton<IHousingCooperativeRepository, MongoHousingCooperativeRepository>();
        builder.Services.AddSingleton<INewsRepository, MongoNewsRepository>();

        // Registrera services
        builder.Services.AddSingleton<IBookingService, BookingService>(); // MAUI skapar BookingService automatiskt och injicerar IBookingRepository som redan är registrerad ovan.
        builder.Services.AddSingleton<IIssueService, IssueService>();
        builder.Services.AddSingleton<IHousingCooperativeService, HousingCooperativeService>();
        builder.Services.AddSingleton(SessionService.GetSession()); // Även här AddSingleton för att använda samma session genom alla olika pages
        builder.Services.AddSingleton<IBookingFacade, BookingFacade>(); 
        builder.Services.AddSingleton<INewsService, NewsService>();

        // Registrera pages                                                                                                            
        builder.Services.AddTransient<HomePage>();                                                                                   
        builder.Services.AddTransient<BookingPage>();                                                                                  
        builder.Services.AddTransient<ManageBookingPage>();                                                                            
        builder.Services.AddTransient<IssueReportPage>();
        builder.Services.AddTransient<NewsPage>();                                                                                   
        
        // Registrera viewmodels                                                                                                      
        builder.Services.AddTransient<BookingViewModel>();
        builder.Services.AddTransient<ManageBookingViewModel>();     
        builder.Services.AddTransient<IssueReportViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<NewsViewModel>();


        return builder.Build();
    }
}