using CommunityToolkit.Mvvm.ComponentModel;


namespace SeiveIT.ViewModels
{
    public partial class RawDataViewModel : ObservableObject
    {
        [ObservableProperty]
        float _phiScale;
        [ObservableProperty]
        string _weight;
        [ObservableProperty]
        double _indWeight;
        [ObservableProperty]
        double _cummWeight;
        [ObservableProperty]
        double _cummPassing;
        [ObservableProperty]
        double _rowNumber;
        [ObservableProperty]
        double _miliScale;
        [ObservableProperty]
        bool _isChecked = false;
        public long Id { get; set; }
    }
}
