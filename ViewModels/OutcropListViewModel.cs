using CommunityToolkit.Mvvm.ComponentModel;
using SeiveIT.Entities;
using SeiveIT.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SeiveIT.ViewModels
{
    public partial class OutcropListViewModel : ObservableObject
    {
        public ObservableCollection<OutcropViewModel>? outcrops { get; set; }
        [ObservableProperty]
        bool _hasOutcrops = false;
        [ObservableProperty]
        public long _projectId;
        

        int page = 0;
        int limit = 25;
        bool hasInit = false;

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (string.Equals(e.PropertyName, nameof(ProjectId)) && ProjectId > 0 && hasInit == false)
            {
                Init();
                hasInit = true;
            }
        }

        void Init()
        {
            var data = Task.Run(async () => await App.serviceManager.OutcropService.GetAllOutcrops(ProjectId, page, limit)).Result;
            outcrops = new ObservableCollection<OutcropViewModel>(data.Select(p => new OutcropViewModel(new Outcrop
            {
                Altitude = p.Altitude,
                Basin = p.Basin,
                Formation = p.Formation,
                Date = p.Date,
                Title = p.Title,
            })));
            HasOutcrops = outcrops.Count > 0;
        }
    }
}
