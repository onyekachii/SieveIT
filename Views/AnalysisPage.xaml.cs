using SeiveIT.Services.Implementation;

namespace SeiveIT.Views;

[QueryProperty(nameof(ProjectId), "pid")]
[QueryProperty(nameof(OutcropId), "oid")]
public partial class AnalysisPage : TabbedPage
{
    public long ProjectId { get; set; }
    public long OutcropId { get; set; }
    public AnalysisPage() => InitializeComponent();
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Children.Any(p => p.Title == "Raw data"))
            return;

        var flexlayout = new FlexLayout();
        flexlayout.Children.Add(new RawDataListView(ProjectId, OutcropId));

        var pageOne = new ContentPage();
        pageOne.Title = "Raw data";
        pageOne.Content = flexlayout;
        if (!Children.Contains(pageOne))
        {
            try
            {
                Children.Add(pageOne);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during page addition
                Console.WriteLine($"Error adding page: {ex.Message}");
            }
        }
    }      
}