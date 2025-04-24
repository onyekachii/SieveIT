using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class RawDataListView : ContentView
{
    public RawDataListView(long projectId, long outcropId)
	{
        InitializeComponent();
        BindingContext = new RawDataListViewModel(projectId, outcropId);
    }
}