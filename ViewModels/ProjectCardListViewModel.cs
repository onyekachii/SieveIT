using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;


namespace SeiveIT.ViewModels
{
    public partial class ProjectCardListViewModel : ObservableObject
    {
        public ObservableCollection<ProjectCardViewModel>? projectCards { get; set; }
        [ObservableProperty]
        private bool _hasProjects = false;

        public ProjectCardListViewModel() => projectCards = [];

    }
}
