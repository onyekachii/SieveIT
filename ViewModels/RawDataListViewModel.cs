using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SeiveIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.ViewModels
{
    public partial class RawDataListViewModel : ObservableObject
    {
        public ObservableCollection<RawDataViewModel> Rows { get; set; }
        [ObservableProperty]
        float _totalWeight;
              
        public RawDataListViewModel()
        {
            Rows = new ObservableCollection<RawDataViewModel>( RawData.GetRows()
                .Select(r => new RawDataViewModel
                {
                    CummWeight = r.CummWeight,
                    IndWeight = r.IndWeight,
                    PhiScale = r.PhiScale,
                    RowNumber = r.RolNum,
                    Weight = r.Weight,
                })
            );
            TotalWeight = Rows.Sum(r => r.Weight);
        }

        [RelayCommand]
        void Run()
        {
            TotalWeight = Rows.Sum(r => r.Weight);
        }
    }
}
