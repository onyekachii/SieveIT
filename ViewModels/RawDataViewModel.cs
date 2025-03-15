using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace SeiveIT.ViewModels
{
    public partial class RawDataViewModel : ObservableObject
    {
        [ObservableProperty]
        double _phiScale;
        [ObservableProperty]
        double _weight;
        [ObservableProperty]
        double _indWeight;
        [ObservableProperty]
        double _cummWeight;
        [ObservableProperty]
        double _rowNumber;

    }
}
