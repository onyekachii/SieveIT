using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SeiveIT.Models;
using System.ComponentModel;

namespace SeiveIT.ViewModels;

public partial class ProjectCardViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private DateTime? _date;
    [ObservableProperty]
    private long _id;
    
    public ProjectCardViewModel(ProjectCardModel model)
    {
        _id = model.Id;
        _title = model.Title;
        _date = model.Date;
    }

    [RelayCommand]
    public async Task GotoProject(long id)
    {
        await Shell.Current.GoToAsync($"project?id={id}");
    }
}
//public class ProjectCardViewModel : INotifyPropertyChanged
//{
//    private ProjectCardModel _props;
//    public ProjectCardModel props 
//    {
//        get => _props;
//        set
//        {
//            if (_props != value)
//            {
//                _props = value;
//                OnPropertyChanged(nameof(props));
//            }
//        }
//    }
//    public ProjectCardViewModel(ProjectCardModel model)
//    {
//        props = model;
//    }

//    public event PropertyChangedEventHandler? PropertyChanged;
//    protected virtual void OnPropertyChanged(string propertyName)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}

