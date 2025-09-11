using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OxyPlot.Axes;
using OxyPlot;
using OxyPlot.Series;
using SeiveIT.Entities;
using SeiveIT.Models;
using SeiveIT.Services.Interface;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace SeiveIT.ViewModels
{
    public partial class RawDataListViewModel : ObservableObject
    {
        IServiceManager _serviceManager = App.serviceManager;
        public long Pid { get; set; }
        public long Oid { get; set; }
        public ObservableCollection<RawDataViewModel> Rows { get; set; }
        public List<RawDataViewModel> RemovedRows { get; set; } = new List<RawDataViewModel>();
        [ObservableProperty]
        double _totalWeight;
        [ObservableProperty]
        double _totalInd;
        [ObservableProperty]
        PlotModel _plotModeler;
        [ObservableProperty]
        PlotModel _plotModeler2;
        [ObservableProperty]
        string _finalResult;
        [ObservableProperty]
        string _finalResult2;
        [ObservableProperty]
        bool _hasCheckedRows;


        public RawDataListViewModel(long pid, long oid)
        {
            Pid = pid;
            Oid = oid;            
            Load();
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            HasCheckedRows = Rows.Any(r => r.IsChecked);
        }

        private PlotModel CreatePlotModel(double min, double max, bool forEngineer)
        {
            try
            {
                var title = forEngineer ? "" : "Cummulative Weight vs Phi";
                var plotModel = new PlotModel { Title = title };
                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = forEngineer ? "Linear X-Axis" : "Cummulative Weight",
                    Minimum = 0,
                    Maximum = 100,
                    MinorGridlineStyle = LineStyle.LongDashDot,
                    MajorGridlineStyle = LineStyle.Solid,
                });
                if (forEngineer)
                {
                    plotModel.Axes.Add(new LogarithmicAxis
                    {
                        Position = AxisPosition.Bottom,
                        Title = "Log Y-Axis",
                        Base = 10,
                        Minimum = min,
                        Maximum = max,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot
                    });
                }
                else
                {
                    plotModel.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Bottom,
                        Title = "Phi Scale",
                        Minimum = min,
                        Maximum = max,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot
                    });
                }
                var series = new LineSeries();
                foreach (var row in Rows)
                {
                    series.Points.Add(new DataPoint(forEngineer ? row.MiliScale : row.PhiScale, forEngineer ? row.CummPassing : row.CummWeight));
                }
                plotModel.Series.Add(series);
                return plotModel;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        double GetXValueForY(double yValue, bool forEngineer = true)
        {
            try
            {
                var series = forEngineer ? PlotModeler.Series : PlotModeler2.Series;
                var lineSeries = series.OfType<LineSeries>().FirstOrDefault();
                if (lineSeries == null)
                    throw new InvalidOperationException("No LineSeries found in the plot.");

                for (int i = 0; i < lineSeries.Points.Count - 1; i++)
                {
                    var p1 = lineSeries.Points[i];
                    var p2 = lineSeries.Points[i + 1];

                    if ((p1.Y <= yValue && p2.Y >= yValue) || (p1.Y >= yValue && p2.Y <= yValue))
                    {
                        double xValue = p1.X + (yValue - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                        return xValue;
                    }
                    //if ((p1.Y <= yValue && p2.Y >= yValue) || (p1.Y >= yValue && p2.Y <= yValue))
                    //{
                    //    // Transform to linearized space if axis is logarithmic
                    //    double y1 = yAxis is LogarithmicAxis ? Math.Log10(p1.Y) : p1.Y;
                    //    double y2 = yAxis is LogarithmicAxis ? Math.Log10(p2.Y) : p2.Y;
                    //    double x1 = xAxis is LogarithmicAxis ? Math.Log10(p1.X) : p1.X;
                    //    double x2 = xAxis is LogarithmicAxis ? Math.Log10(p2.X) : p2.X;

                    //    double yTarget = yAxis is LogarithmicAxis ? Math.Log10(yValue) : yValue;

                    //    // Linear interpolation in transformed space
                    //    double xInterp = x1 + (yTarget - y1) * (x2 - x1) / (y2 - y1);

                    //    // Convert back from log if necessary
                    //    return xAxis is LogarithmicAxis ? Math.Pow(10, xInterp) : xInterp;
                    //}
                }

                throw new InvalidOperationException("No intersection found for the specified Y value.");
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("An error occured", ex.Message, "OK");
                return 0;
            }
        }

        void Load()
        {
            List<SeiveData> data = Task.Run(async ()=> await _serviceManager.RawDataService.Get(Pid, Oid)).Result;
            if (data.Count > 0)
            {
                Rows = new ObservableCollection<RawDataViewModel>(data.OrderBy(d => d.PhiScale).Select(r => new RawDataViewModel
                {
                    PhiScale = r.PhiScale,
                    MiliScale = Math.Round( Math.Pow(2, -r.PhiScale), 3, MidpointRounding.AwayFromZero ),
                    Weight = r.Weight.ToString(),
                    Id = r.Id
                }));
                Run();
            }
            else
            {
                Rows = new ObservableCollection<RawDataViewModel>(RawData.GetRows()
                .Select(r => new RawDataViewModel
                {
                    MiliScale = Math.Round(Math.Pow(2, -r.PhiScale), 3, MidpointRounding.AwayFromZero),
                    CummWeight = r.CummWeight,
                    IndWeight = r.IndWeight,
                    PhiScale = r.PhiScale,
                    RowNumber = r.RolNum,
                    Weight = r.Weight.ToString(),
                    CummPassing = r.CummPassing                    
                }));
                foreach (var item in Rows)
                {
                    item.PropertyChanged += OnPropertyChanged;
                }
            }
        }

        [RelayCommand]
        void Run()
        {
            try
            {
                if(Rows.Any(r => string.IsNullOrEmpty(r.Weight)))
                {
                    Shell.Current.DisplayAlert("Invalid data", $"Weights cannot be empty: Check weights input", "OK");
                    return;
                }
                TotalWeight = Math.Round(Rows.Sum(r => float.Parse(r.Weight)), 1);
                double totalInd = 0;
                double prevRow = 0;
                foreach (var row in Rows)
                {
                    row.MiliScale = Math.Round(Math.Pow(2, -row.PhiScale), 3, MidpointRounding.AwayFromZero);
                    row.IndWeight = Math.Round((100 * float.Parse(row.Weight)) / TotalWeight, 3);
                    row.CummWeight = 0;
                    row.CummWeight = Math.Round(prevRow + row.IndWeight, 3);
                    prevRow = row.CummWeight;
                    totalInd += row.IndWeight;
                }
                TotalInd = Math.Round(totalInd);
                foreach (var row in Rows)
                {
                    row.CummPassing = Math.Round(TotalInd - row.CummWeight, 3);
                    row.PropertyChanged += OnPropertyChanged;
                }
                var phiValues = Rows.Select(r => r.PhiScale);
                var mmValues = Rows.Select(r => r.MiliScale);

                BuildLogVsCummPassing(mmValues);
                BuildLogVsCummWeight(phiValues);
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("An error occured", ex.Message, "OK");
                return;
            }
        }
        void BuildLogVsCummPassing(IEnumerable<double> phiValues)
        {
            PlotModeler = CreatePlotModel(phiValues.Min(), phiValues.Max(), true);
            var d10 = GetXValueForY(10);
            var d30 = GetXValueForY(30);
            var d60 = GetXValueForY(60);
            var cc = Math.Pow(d30, 2) / (d10 * d60);
            var cu = d60 / d10;
            bool isWellGradedGravel = cu > 4 && cc > 1 && cc < 3;
            bool isWellGradedSand = cu > 6 && cc > 1 && cc < 3;
            bool isUniformGraded = cu == 1 && cc == 1;
            StringBuilder results = new StringBuilder();

            results.Append($"Coefficient of Curvature: {cc}\n");
            results.Append($"Uniformity: {cu}\n");
            results.Append($"D10: {d10}\n");
            results.Append($"D30: {d30}\n");
            results.Append($"D60: {d60}\n\n");
            results.Append($"Based on calculations, Sample is characteristic of:\n");
            if (isWellGradedGravel)
                results.Append("well graded gravel and/or ");
            else
                results.Append($"poorly graded gravel and/or ");
            if (isWellGradedSand)
                results.Append("well graded sand. ");
            else
                results.Append($"poorly graded sand. ");
            if (isUniformGraded)
                results.Append("Uniform grading is conclusive.");
            FinalResult = results.ToString();
        }
        void BuildLogVsCummWeight(IEnumerable<float> phiValues)
        {
            PlotModeler2 = CreatePlotModel(phiValues.Min(), phiValues.Max(), false);
            if (PlotModeler2 is null)
                return;

            var d95 = GetXValueForY(95, false);
            var d84 = GetXValueForY(84, false);
            var d75 = GetXValueForY(75, false);
            var d50 = GetXValueForY(50, false);
            var d25 = GetXValueForY(25, false);
            var d16 = GetXValueForY(16, false);
            var d5 = GetXValueForY(5, false);
            var graphicMeanSize = (d84 + d50 + d16) / 3;
            var standardDeviation = ((d84 - d16) / 4) + ((d95 - d5) / 6.6);
            double part1 = (d16 + d84 - 2.0 * d50) / (2.0 * (d84 - d16));
            double part2 = (d5 + d95 - 2.0 * d50) / (2.0 * (d95 - d5));
            var inclusiveGraphicSkewness = part1 + part2;
            var graphicKurtosis = (d95 - d5) / (2.44 * (d75 - d25));

            StringBuilder results = new StringBuilder();

            results.Append($"Graphic Mean Size: {graphicMeanSize}\n");
            results.Append($"Standard Deviation: {standardDeviation}\n");
            results.Append($"Skewness: {inclusiveGraphicSkewness}\n");
            results.Append($"Kurtosis: {graphicKurtosis}\n");
            results.Append($"D95: {d95}\n");
            results.Append($"D84: {d84}\n");
            results.Append($"D75: {d75}\n");
            results.Append($"D50: {d50}\n");
            results.Append($"D25: {d25}\n");
            results.Append($"D16: {d16}\n");
            results.Append($"D5: {d5}\n\n");
            results.Append($"Sample is characteristic of:\n");
            results.Append($"{WentworthClassification(graphicMeanSize)}, Overall {SortClassification(standardDeviation)}, {DepositionVelocity(inclusiveGraphicSkewness)} and {KurtosisClassification(graphicKurtosis)}. ");

            FinalResult2 = results.ToString();
        }
        string WentworthClassification(double mean){
            if (mean < -1)
                return "Gravel";
            else if (mean >= -1 && mean < 0)
                return "Very Course Sand";
            else if (mean >= 0 && mean < 1)
                return "Coarse Sand";
            else if (mean >= 1 && mean < 2)
                return "Medium Sand";
            else if (mean >= 2 && mean < 3)
                return "Fine Sand";
            else if (mean >= 3 && mean < 4)
                return "Very Fine Sand";
            else if (mean >= 4 && mean < 8)
                return "Silt";
            else
                return "Clay";
            //            Wentworth Grain Size Scale Wentworth, C.K. (1922).A scale of grade and class terms for clastic sediments.
            //            Journal of Geology, 30(5), 377–392.
            //            https://www.journals.uchicago.edu/doi/10.1086/622910
        }
        string SortClassification(double sd)
        {
            if (sd < 0.35)
                return "Very Well Sorted";
            else if (sd >= 0.35 && sd < 0.5)
                return "Well Sorted";
            else if (sd >= 0.5 && sd < 0.71)
                return "Moderately Well Sorted";
            else if (sd >= 0.71 && sd < 1)
                return "Moderately Sorted";
            else if (sd >= 1 && sd < 2)
                return "Poorly Sorted";
            else if (sd >= 2 && sd < 4)
                return "Very Poorly Sorted";
            else
                return "Extremely Poorly Sorted";
            //            Folk, R.L., &Ward, W.C. (1957).Brazos River bar: a study in the significance of grain size parameters.
            //            Journal of Sedimentary Petrology, 27(1), 3–26.
            //            https://chooser.crossref.org/?doi=10.1306%2F74D70646-2B21-11D7-8648000102C1865D
        }
        string DepositionVelocity(double skew)
        {
            if (skew >= -1 && skew < -0.3)
                return "very significant episodes of higher flow energy from Very Coarse-skewed";
            else if (skew >= -0.3 && skew < -0.1)
                return "occasional high-energy burst from Coarse-skewed";
            else if (skew >= -0.1 && skew <= 0.1)
                return "Near Symmetrical Skew (Balanced distribution)";
            else if (skew >= 0.1 && skew < 0.3)
                return "ocassional low-energy from Fine-skewed (Most grains are coarse/medium, but with an extra share of finer particles)";
            else if (skew >= 0.3 && skew <= 1)
                return "very significant episodes of lower flow energy from Very Fine-skewed (Dominated by coarser grains, but with a strong tail of much finer grains)";
            return string.Empty;
//            Folk, R.L., &Ward, W.C. (1957).Brazos River bar: a study in the significance of grain size parameters.
//            Journal of Sedimentary Petrology, 27(1), 3–26.
//            https://pubs.geoscienceworld.org/sepm/jsedres/article-abstract/27/1/3/95232/Brazos-River-bar-Texas-a-study-in-the-significance?redirectedFrom=fulltext
        }
        string KurtosisClassification(double kurtosis)
        {
            if (kurtosis < 0.67)
                return "Very Platykurtic (flat-topped curve)";
            else if (kurtosis >= 0.67 && kurtosis < 0.90)
                return "Platykurtic (broad, flat peak)";
            else if (kurtosis >= 0.90 && kurtosis <= 1.11)
                return "Mesokurtic (normal-like distribution)";
            else if (kurtosis > 1.11 && kurtosis <= 1.50)
                return "Leptokurtic (peaked curve)";
            else if (kurtosis > 1.50 && kurtosis <= 3.00)
                return "Very Leptokurtic (strongly peaked)";
            else
                return "Extremely Leptokurtic (extremely sharp peak)";

            // Folk, R. L., & Ward, W. C. (1957).
            // Brazos River bar: a study in the significance of grain size parameters.
            // Journal of Sedimentary Petrology, 27(1), 3–26.
        }

        [RelayCommand]
        async Task Save()
        {
            try
            {
                var data = Rows.Select(r => new SeiveData
                {
                    Id = r.Id,
                    PhiScale = r.PhiScale,
                    Weight = float.Parse(r.Weight),
                    OutcropId = Oid,
                    ProjectId = Pid,
                }).ToList();
                await _serviceManager.RawDataService.UpsertSeiveData(data);
                if(RemovedRows.Count >= 1)
                {
                    var removeData = RemovedRows.Select(r => new SeiveData
                    {
                        Id = r.Id,
                        PhiScale = r.PhiScale,
                        Weight = float.Parse(r.Weight),
                        OutcropId = Oid,
                        ProjectId = Pid,
                    }).ToList();
                    await _serviceManager.RawDataService.DeleteAll(removeData);
                    RemovedRows.Clear();
                }
                await Toast.Make("Project saved").Show();
                //await Shell.Current.GoToAsync($"project?id={proj.Id}");
            }
            catch (SQLite.SQLiteException e)
            {
                var a = e.GetType();
                string errMsg = e.Message.Contains("UNIQUE constraint failed") ? "Title already exists" : "Unable to Save data, send us a mail";
                await Toast.Make(errMsg).Show();
            }
            catch (Exception e)
            {
                await Toast.Make("An error has occured, send us a mail").Show();
            }
        }

        [RelayCommand]
        void RemoveRows()
        {
            for (int i = Rows.Count - 1; i >= 0; i--)
            {
                if (Rows[i].IsChecked)
                {
                    RemovedRows.Add(Rows[i]);
                    Rows.RemoveAt(i);
                }
            }
        }

        [RelayCommand]
        void AddRow(bool Up)
        {
            if(Up)
                Rows.Insert(0, new RawDataViewModel());
            else
                Rows.Add(new RawDataViewModel());

        }
    }
}