using SeiveIT.Views;

namespace SeiveIT
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("project", typeof(ProjectPage));
        }
    }
}
