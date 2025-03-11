using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class OutcropListView : ContentView
{
    public OutcropListViewModel OutcropListViewModel;

    public OutcropListView()
	{
		InitializeComponent();
        OutcropListViewModel = new OutcropListViewModel();
        BindingContext = OutcropListViewModel;
    }

}