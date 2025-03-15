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
        double _totalWeight;
        [ObservableProperty]
        double _totalInd;
              
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
            TotalWeight = Math.Round(Rows.Sum(r => r.Weight), 1);
            double prevRow = 0;
            foreach (var row in Rows)
            {
                row.IndWeight = Math.Round( (100 * row.Weight) / TotalWeight, 2);
                row.CummWeight = 0;
                row.CummWeight = Math.Round(prevRow + row.IndWeight, 2);
                prevRow = row.CummWeight;
            }
        
            TotalInd = Math.Round(Rows.Sum(r => r.IndWeight));            
        }
    }
}
