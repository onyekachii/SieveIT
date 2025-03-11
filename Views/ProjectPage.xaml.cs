using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using SeiveIT.ViewModels;
using SeiveIT.Views.popups;
using System;

namespace SeiveIT.Views;

[QueryProperty(nameof(id), "id")]
public partial class ProjectPage : ContentPage
{
    public long id { get; set; }
    public ProjectPage()
	{
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        outcropListView.OutcropListViewModel.ProjectId = id;
    }
    public void displayPopup(object sender, EventArgs e) => this.ShowPopup(new AddOutcropPopup(id));
}