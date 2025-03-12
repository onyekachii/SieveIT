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
    public ProjectPage() =>	
        InitializeComponent();
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!mainlayout.Children.OfType<OutcropListView>().Any())        
            mainlayout.Add(new OutcropListView(ProjectId));        
    }

    public void displayPopup(object sender, EventArgs e) => this.ShowPopup(new AddOutcropPopup(ProjectId));
}