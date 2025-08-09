using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class OutcropListView : ContentView
{
    public OutcropListView(long projectid)
	{
		InitializeComponent();
        OutcropListViewModel OutcropListViewModel = new OutcropListViewModel();
        OutcropListViewModel.ProjectId = projectid;
        BindingContext = OutcropListViewModel;
        MyItemsLayout.Span = OutcropListViewModel?.outcrops?.Count > 1 ? 2 : 1;
    }

}
