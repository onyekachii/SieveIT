using CommunityToolkit.Maui.Views;
using SeiveIT.ViewModels;

namespace SeiveIT.Views.popups;

public partial class AddOutcropPopup : Popup
{
	public AddOutcropPopup(long projectid)
	{
		InitializeComponent();
        BindingContext = new AddOutcropPopupViewModel(projectid, async () => await ClosePopup());

    }
    async Task ClosePopup()
    {
        await CloseAsync();
    }
}