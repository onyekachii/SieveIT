namespace SeiveIT.Views;

public partial class RawDataPage : ContentPage
{
    public long Proj { get; set; }
    public long OutId { get; set; }
    public RawDataPage(long ProjectId, long OutcropId)
	{
        Proj = ProjectId;
        OutId = OutcropId;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!mainLayout.Children.OfType<RawDataListView>().Any())
            mainLayout.Children.Add(new RawDataListView(Proj, OutId));
    }
}