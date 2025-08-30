using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class OutcropListView : ContentView
{
    static OutcropListViewModel OutcropListViewModel;

    public static readonly BindableProperty ProjectIdProperty =
      BindableProperty.Create(
          nameof(ProjectId),
          typeof(long),
          typeof(OutcropListView),
          default(long),
          propertyChanged: OnProjectIdChanged);

    public long ProjectId
    {
        get => (long)GetValue(ProjectIdProperty);
        set => SetValue(ProjectIdProperty, value);
    }
    public OutcropListView()
    {
        InitializeComponent();
        BindingContext = OutcropListViewModel;

    }

    private static void OnProjectIdChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (OutcropListView)bindable;
        if (newValue is long projectId)
        {
            OutcropListViewModel = new OutcropListViewModel();
            OutcropListViewModel.ProjectId = projectId;
        }
    }
}
