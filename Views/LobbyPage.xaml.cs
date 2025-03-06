using CommunityToolkit.Maui.Views;
using SeiveIT.Views.popups;

namespace SeiveIT.Views;

public partial class LobbyPage : ContentPage
{
	public LobbyPage()
	{
		InitializeComponent();
	}

	public void displayPopup(object sender, EventArgs e)
    {
        var popup = new AddprojectPopup();
        this.ShowPopup(popup);
    }
}