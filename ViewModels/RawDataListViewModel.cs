using CommunityToolkit.Mvvm.ComponentModel;
using SeiveIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.ViewModels
{
    public class RawDataListViewModel : ObservableObject
    {
        public ObservableCollection<RawDataViewModel> Rows { get; set; }

        public RawDataListViewModel()
        {
            Rows = new ObservableCollection<RawDataViewModel>( RawData.GetRows().Select(r => new RawDataViewModel
            {
                CummWeight = r.CummWeight,
                IndWeight = r.IndWeight,
                PhiScale = r.PhiScale,
                RowNumber = r.RolNum,
                Weight = r.Weight,
            }) );
        }
    }
}
