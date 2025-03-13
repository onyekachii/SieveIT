using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class RawDataRows : ContentView
{
	public RawDataRows()
	{
		InitializeComponent();
		BindingContext = new RawDataViewModel();
    }
}