﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SeiveIT.Models;

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


