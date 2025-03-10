using CommunityToolkit.Mvvm.ComponentModel;
using SeiveIT.Entities;
using System.Collections.ObjectModel;

namespace SeiveIT.ViewModels
{
    public partial class ProjectCardListViewModel : ObservableObject
    {
        public ObservableCollection<ProjectCardViewModel>? projectCards { get; set; }
        [ObservableProperty]
        private bool _hasProjects = false;
        int page = 0;
        int limit = 25;

        void Get()
        {
            var projects = Task.Run(async ()=> await App.serviceManager.ProjectService.GetAllProject(page, limit)).Result;
            projectCards = new ObservableCollection<ProjectCardViewModel>(projects.Select(p => new ProjectCardViewModel(new Models.ProjectCardModel
            {
                Date = p.Date,
                Title = p.Title,
            })));
            HasProjects = projectCards.Count > 0;            
        }
        public ProjectCardListViewModel()
        {
            Get();
        }
    }
}
