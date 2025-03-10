using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using SeiveIT.Services.Interface;
using SeiveIT.Entities;

namespace SeiveIT.ViewModels
{
    public partial class AddprojectPopupViewModel : ObservableObject
    {
        Func<Task> closeCallback;
        readonly IServiceManager _serviceManager;
        public AddprojectPopupViewModel(Func<Task> closeAction, IServiceManager sm)
        {
            closeCallback = closeAction;
            _serviceManager = sm;
        }

        [ObservableProperty]
        private string? _title;
        [ObservableProperty]
        private DateTime? _date = DateTime.Today;
        [ObservableProperty]
        private string? _latitudeH;
        [ObservableProperty]
        private string? _latitudeL;
        [ObservableProperty]
        private string? _longitudeH;
        [ObservableProperty]
        private string? _longitudeL;

        [ObservableProperty]
        private string? _notes;

        [RelayCommand]
        private async Task Save()
        {
            try
            {
                await _serviceManager.ProjectService.AddProject(new Project
                {
                    Date = Date,
                    Latitude = $"{LatitudeL}-{LatitudeH}",
                    Longitude = $"{LongitudeL}-{LongitudeH}",
                    Notes = Notes,
                    Title = Title
                });

                await Toast.Make("Project saved").Show();
                await closeCallback();
            }
        
            catch (SQLite.SQLiteException e)
            {
                var a = e.GetType();
                string errMsg = e.Message.Contains("UNIQUE constraint failed") ? "Title already exists" : "Unable to Save data, send us a mail";
                await Toast.Make(errMsg).Show();
            }
            catch(Exception e)
            {
                await Toast.Make("An error has occured, send us a mail").Show();
            }
        }

        [RelayCommand()]
        private async Task Cancel() =>       
            await closeCallback();
        
    }
}
