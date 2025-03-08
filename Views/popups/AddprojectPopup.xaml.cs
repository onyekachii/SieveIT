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
        _serviceManager = sm;
        BindingContext = new AddprojectPopupViewModel(async () => await CloseAsync(), _serviceManager);
    }
}