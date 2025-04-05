using SeiveIT.Services.Implementation;

namespace SeiveIT.Views;

[QueryProperty(nameof(ProjectId), "pid")]
[QueryProperty(nameof(OutcropId), "oid")]
public partial class AnalysisPage : TabbedPage
{
    public long ProjectId { get; set; }
    public long OutcropId { get; set; }
    public AnalysisPage()
    {
        InitializeComponent();
        Loaded += AnalysisPage_Loaded;
    }

    private void AnalysisPage_Loaded(object? sender, EventArgs e)
    {
        var flexlayout = new FlexLayout();
        flexlayout.Children.Add(new RawDataListView(ProjectId, OutcropId));

        var pageOne = new ContentPage();
        pageOne.Title = "Raw data";
        pageOne.Content = flexlayout;
        if (!Children.Any(c => c == pageOne))
            Children.Add(pageOne);
    }
}