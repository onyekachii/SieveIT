using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SeiveIT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.ViewModels
{
    public partial class OutcropViewModel : ObservableObject
    {
        [ObservableProperty]
        Outcrop _outcrop;

        public OutcropViewModel(Outcrop o)
        {
            _outcrop = o;
        }

        [RelayCommand]
        async Task GotoAnalyse()
        {
            await Shell.Current.GoToAsync($"new?pid={_outcrop.ProjectId}&oid={_outcrop.Id}");

            //if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            //    await Shell.Current.GoToAsync($"new?pid={_outcrop.ProjectId}&oid={_outcrop.Id}");
            //else
            //    await Shell.Current.GoToAsync($"analyse?pid={_outcrop.ProjectId}&oid={_outcrop.Id}");

        }
    }
}
