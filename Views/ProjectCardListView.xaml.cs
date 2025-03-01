using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class ProjectCardListView : ContentView
{
	public ProjectCardListView()
	{
		InitializeComponent();
		BindingContext = new ProjectCardListViewModel();
	}
}