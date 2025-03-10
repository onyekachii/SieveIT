using CommunityToolkit.Maui.Views;
using SeiveIT.Services.Interface;
using SeiveIT.ViewModels;

namespace SeiveIT.Views.popups;

public partial class AddprojectPopup : Popup
{
    IServiceManager _serviceManager;

    public AddprojectPopup(IServiceManager sm)
	{
        InitializeComponent();
        BindingContext = new AddprojectPopupViewModel(async () => await ClosePopup(), sm);
    }

    async Task ClosePopup()
    {
        await CloseAsync();
    }
}