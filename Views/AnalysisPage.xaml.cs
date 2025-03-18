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
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        var pageOne = new RawDataPage(ProjectId, OutcropId);
        pageOne.Title = "Raw data";
        if (!Children.OfType<RawDataPage>().Any())
            Children.Add(pageOne);
    }

}