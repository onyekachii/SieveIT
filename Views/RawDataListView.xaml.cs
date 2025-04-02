using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Maui.Skia;
using OxyPlot.Series;
using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class RawDataListView : ContentView
{
    public RawDataListView(long projectId, long outcropId)
	{
        InitializeComponent();
        plot.Model = CreatePlotModel();
        BindingContext = new RawDataListViewModel(projectId, outcropId);
    }
   
    private PlotModel CreatePlotModel()
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
            Base = 10, // Logarithm base (10 for log10, 2 for log2, etc.)
            Minimum = 1,  // Minimum value (10^0 = 1)
            Maximum = 10000, // Maximum value (10^4 = 10000)
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot
        });
        var series = new LineSeries();
        series.Points.Add(new DataPoint(1, 1));
        series.Points.Add(new DataPoint(2, 10));
        series.Points.Add(new DataPoint(3, 100));
        series.Points.Add(new DataPoint(4, 1000));
        series.Points.Add(new DataPoint(5, 10000));
        plotModel.Series.Add(series);
        // Add your axes and series
        return plotModel;
    }
}