using CommunityToolkit.Mvvm.ComponentModel;
using SeiveIT.Models;
using System.ComponentModel;

namespace SeiveIT.ViewModels;

public partial class ProjectCardViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private DateTime? _date;
    
    public ProjectCardViewModel(ProjectCardModel model)
    {
        _title = model.Title;
        _date = model.Date;
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

