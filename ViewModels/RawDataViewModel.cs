using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace SeiveIT.ViewModels
{
    public partial class RawDataViewModel : ObservableObject
    {
        [ObservableProperty]
        float _phiScale;
        [ObservableProperty]
        float _weight;
        [ObservableProperty]
        float _indWeight;
        [ObservableProperty]
        float _cummWeight;
        [ObservableProperty]
        float _rowNumber;

    }
}
