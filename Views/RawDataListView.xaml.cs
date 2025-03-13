using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class RawDataListView : ContentView
{
	public RawDataListView()
	{
		InitializeComponent();
		BindingContext = new RawDataListViewModel();
	}
}