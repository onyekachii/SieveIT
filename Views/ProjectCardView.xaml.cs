using SeiveIT.Models;
using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class ProjectCardView : ContentView
{
	public ProjectCardView()
	{
		InitializeComponent();
		BindingContext = new ProjectCardViewModel(new ProjectCardModel());
	}

}