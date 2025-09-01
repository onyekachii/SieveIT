using CommunityToolkit.Maui.Alerts;
using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class ProjectCardListView : ContentView
{
	public ProjectCardListView()
	{
		InitializeComponent();
		BindingContext = new ProjectCardListViewModel();
		var vm = BindingContext as ProjectCardListViewModel;
	}
}