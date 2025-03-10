using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SeiveIT.Repository;
using SeiveIT.Repository.Implementation;
using SeiveIT.Repository.Interface;
using SeiveIT.Services.Implementation;
using SeiveIT.Services.Interface;

namespace SeiveIT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine( FileSystem.AppDataDirectory, "seiveIT.db3" );
            builder.Services.AddSingleton<DatabaseManager>(s => ActivatorUtilities.CreateInstance<DatabaseManager>(s, dbPath));
            builder.Services.AddSingleton<IRepositoryManager, RepositoryManager>();
            builder.Services.AddSingleton<IServiceManager, ServiceManager>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}


//builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//builder.Services.AddScoped<Func<ICompanyRepository>>(sp => () => sp.GetRequiredService<ICompanyRepository>());
//builder.Services.AddScoped<Func<IEmployeeRepository>>(sp => () => sp.GetRequiredService<IEmployeeRepository>());

//builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

//public class SomeService
//{
//    private readonly Lazy<IEmployeeRepository> _lazyEmployeeRepo;

//    public SomeService(Lazy<IEmployeeRepository> lazyEmployeeRepo)
//    {
//        _lazyEmployeeRepo = lazyEmployeeRepo;
//    }

//    public void DoWork()
//    {
//        var employeeRepo = _lazyEmployeeRepo.Value; 
//        employeeRepo.SomeMethod();
//    }
//}
