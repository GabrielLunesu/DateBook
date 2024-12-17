namespace ui.Views.Likes;

public class LikeModel
{
    public string ImageSource { get; set; }
    public string Name { get; set; }
}

public partial class LikesPage : ContentPage
{
    public List<LikeModel> Likes { get; set; }

    public LikesPage()
    {
        InitializeComponent();
        LoadLikes();
        BindingContext = this;
    }

    private void LoadLikes()
    {
        Likes = new List<LikeModel>
        {
            new LikeModel { ImageSource = "headshot.jpeg", Name = "James, 28" },
            new LikeModel { ImageSource = "headshot.jpeg", Name = "Mike, 25" },
            new LikeModel { ImageSource = "headshot.jpeg", Name = "Sarah, 24" },
            new LikeModel { ImageSource = "headshot.jpeg", Name = "Nina, 26" },
            new LikeModel { ImageSource = "headshot.jpeg", Name = "Tom, 29" },
            new LikeModel { ImageSource = "headshot.jpeg", Name = "David, 27" }
        };
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
} 