using SeiveIT.Views;

namespace SeiveIT
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("project", typeof(ProjectPage));
            Routing.RegisterRoute("analyse", typeof(AnalysisPage));
            Routing.RegisterRoute("new", typeof(NewPage1));
        }
    }
}
