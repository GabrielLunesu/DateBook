using ui.ViewModels;

namespace ui.Views.Chat;

public partial class ChatPageViewExample : ContentPage
{
	public ChatPageViewExample(ChatPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void OnChatSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ChatModel selectedChat)
        {
            await Navigation.PushAsync(new ChatDetailPage(selectedChat));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}