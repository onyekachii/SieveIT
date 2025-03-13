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
}