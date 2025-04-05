using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OxyPlot.Axes;
using OxyPlot;
using OxyPlot.Maui.Skia;
using OxyPlot.Series;
using SeiveIT.Entities;
using SeiveIT.Models;
using SeiveIT.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SeiveIT.ViewModels
{
    public partial class RawDataListViewModel : ObservableObject
    {
        IServiceManager _serviceManager = App.serviceManager;
        public long Pid { get; set; }
        public long Oid { get; set; }
        public ObservableCollection<RawDataViewModel> Rows { get; set; }
        [ObservableProperty]
        double _totalWeight;
        [ObservableProperty]
        double _totalInd;
        [ObservableProperty]
        PlotModel _plotModeler;
        [ObservableProperty]
        string _finalResult;

        public RawDataListViewModel(long pid, long oid)
        {
            Pid = pid;
            Oid = oid;
            Load();
        }

        private PlotModel CreatePlotModel(double minPhi, double maxPhi)
        {
            var plotModel = new PlotModel { Title = "Semi-Log Plot" };
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Linear X-Axis",
                Minimum = 0,
                Maximum = 100,
                MinorGridlineStyle = LineStyle.LongDashDot,
                MajorGridlineStyle = LineStyle.Solid,
            });
            plotModel.Axes.Add(new LogarithmicAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Log Y-Axis",
                Base = 10,
                Minimum = minPhi,
                Maximum = maxPhi,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });
            var series = new LineSeries();
            foreach (var row in Rows)
            {
                series.Points.Add(new DataPoint(row.MiliScale, row.CummPassing));
            }
            plotModel.Series.Add(series);
            return plotModel;
        }

        double GetXValueForY(double yValue)
        {
            var lineSeries = PlotModeler.Series.OfType<LineSeries>().FirstOrDefault();
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
            }

            throw new InvalidOperationException("No intersection found for the specified Y value.");
        }

        void Load()
        {
            List<SeiveData> data = Task.Run(async ()=> await _serviceManager.RawDataService.Get(Pid, Oid)).Result;
            if (data.Count > 0)
            {
                Rows = new ObservableCollection<RawDataViewModel>(data.OrderBy(d => d.PhiScale).Select(r => new RawDataViewModel
                {
                    PhiScale = r.PhiScale,
                    MiliScale = Math.Pow(2, -r.PhiScale),
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
                    MiliScale = Math.Pow(2, -r.PhiScale),
                    CummWeight = r.CummWeight,
                    IndWeight = r.IndWeight,
                    PhiScale = r.PhiScale,
                    RowNumber = r.RolNum,
                    Weight = r.Weight.ToString(),
                    CummPassing = r.CummPassing
                }));
            }
        }

        [RelayCommand]
        void Run()
        {
            TotalWeight = Math.Round(Rows.Sum(r => float.Parse(r.Weight)), 1);
            double totalInd = 0;
            double prevRow = 0;
            foreach (var row in Rows)
            {
                row.IndWeight = Math.Round( (100 * float.Parse(row.Weight)) / TotalWeight, 3);
                row.CummWeight = 0;
                row.CummWeight = Math.Round(prevRow + row.IndWeight, 3);
                prevRow = row.CummWeight;
                totalInd += row.IndWeight;
            }
            TotalInd = Math.Round(totalInd);  
            foreach(var row in Rows)
            {
                row.CummPassing = Math.Round(TotalInd - row.CummWeight, 3);
            }
            var phiValues = Rows.Select(r => r.MiliScale);
            PlotModeler = CreatePlotModel(phiValues.Min(), phiValues.Max());
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
                    ProjectId = Pid
                }).ToList();
                await _serviceManager.RawDataService.UpsertSeiveData(data);                                           

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
    }
}