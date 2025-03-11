using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SeiveIT.Entities;
using SeiveIT.Services.Interface;

namespace SeiveIT.ViewModels
{
    public partial class AddOutcropPopupViewModel : ObservableObject
    {
        readonly IServiceManager _sm;
        Func<Task> _closeAction;
        readonly long _projectId;
        public AddOutcropPopupViewModel(long projectid, Func<Task> closeAction)
        {
            _sm = App.serviceManager;
            _projectId = projectid;
            _closeAction = closeAction;
        }

        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private DateTime? _date = DateTime.Today;
        [ObservableProperty]
        private string _basin;
        [ObservableProperty]
        private int _altitude;
        [ObservableProperty]
        private string _formation;

        [RelayCommand]
        private async Task Save()
        {
            try
            {
                var outcrop = await _sm.OutcropService.AddOutcrop(new Outcrop
                {
                    Date = Date,
                    Basin = Basin,
                    Altitude = Altitude,
                    Formation = Formation,
                    Title = Title,
                    ProjectId = _projectId                
                });

                await Toast.Make("Outcrop saved").Show();
                await Shell.Current.GoToAsync("..");
            }

            catch (SQLite.SQLiteException e)
            {
                var a = e.GetType();
                string errMsg = e.Message.Contains("UNIQUE constraint failed") ? "Title already exists" : "Unable to Save data, send us a mail";
                await Toast.Make(errMsg).Show();
            }
            catch (Exception e)
            {
                await Toast.Make("An error has occured, send us a mail").Show();
            }
        }

        [RelayCommand()]
        private async Task Cancel() =>
           await _closeAction();
    }
}
