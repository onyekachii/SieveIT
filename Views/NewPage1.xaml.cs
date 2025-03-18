namespace SeiveIT.Views;

[QueryProperty(nameof(ProjectId), "pid")]
[QueryProperty(nameof(OutcropId), "oid")]
public partial class NewPage1 : ContentPage
{
    public long ProjectId { get; set; }
    public long OutcropId { get; set; }
    public NewPage1()
	{
        InitializeComponent();
        OnTab1Clicked(null, null);
    }
    private void OnTab1Clicked(object sender, EventArgs e)
    {       
        if (!TabContent.Children.OfType<RawDataListView>().Any())
            TabContent.Children.Add(new RawDataListView(ProjectId, OutcropId));
        
        //further implementation needed for proper tab
    }       
}