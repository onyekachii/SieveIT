using SeiveIT.Services.Interface;

namespace SeiveIT
{
    public partial class App : Application
    {
        public static IServiceManager serviceManager;
        public App(IServiceManager sm)
        {
            InitializeComponent();
            serviceManager = sm;
            MainPage = new AppShell();
        }
    }
}
