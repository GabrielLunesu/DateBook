namespace ui.Views.Home.Components;

public partial class FilterView : ContentPage
{
    public FilterView()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnApplyClicked(object sender, EventArgs e)
    {
        // Handle filter application
        await Navigation.PopAsync();
    }
} 