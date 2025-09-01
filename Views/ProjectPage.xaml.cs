using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using SeiveIT.ViewModels;
using SeiveIT.Views.popups;
using System;

namespace SeiveIT.Views;

[QueryProperty(nameof(ProjectId), "id")]
public partial class ProjectPage : ContentPage
{
    public long ProjectId { get; set; }
    public ProjectPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        OutcropContainer.ProjectId = 0;
        OutcropContainer.ProjectId = ProjectId;
        InitializeComponent();

    }

    public void displayPopup(object sender, EventArgs e) => this.ShowPopup(new AddOutcropPopup(ProjectId));
}