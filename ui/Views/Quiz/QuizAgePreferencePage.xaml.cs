namespace ui.Views.Quiz;

public partial class QuizAgePreferencePage : ContentPage
{
   

    public QuizAgePreferencePage()
    {
        InitializeComponent();
        
    }

     private void OnMinSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Ensure min value doesn't exceed max value
        if (e.NewValue > MaxSlider.Value)
        {
            MinSlider.Value = MaxSlider.Value;
        }
    }

    private void OnMaxSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Ensure max value doesn't go below min value
        if (e.NewValue < MinSlider.Value)
        {
            MaxSlider.Value = MinSlider.Value;
        }
    }
} 