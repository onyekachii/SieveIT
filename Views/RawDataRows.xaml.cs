using SeiveIT.ViewModels;

namespace SeiveIT.Views;

public partial class RawDataRows : ContentView
{
	public RawDataRows()
	{
		InitializeComponent();
		BindingContext = new RawDataViewModel();
    }
    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        try
        {
            if (!string.IsNullOrEmpty(entry.Text))
                float.Parse(entry.Text);
        }
        catch (Exception ex)
        {
            entry.Text = e.OldTextValue;
        }        
    }

}