using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ui.Views.Chat;

namespace ui.ViewModels
{
    public partial class ChatPageViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ChatModel> chats;

        public ChatPageViewModel()
        {
            LoadChats();
        }
        private void LoadChats()
        {
            chats = new ObservableCollection<ChatModel>()
        {
            new ChatModel
            {
                ImageSource = "headshot.jpeg",
                Name = "Siliva",
                LastMessage = "I'm not a hoarder but I really...",
                Time = "11:30"
            },
            new ChatModel
            {
                ImageSource = "headshot.jpeg",
                Name = "Lucy",
                LastMessage = "Is your body from Mcdonals",
                Time = "13:51"
            },
            new ChatModel
            {
                ImageSource = "headshot.jpeg",
                Name = "Lucy",
                LastMessage = "Is your body from Mcdonals",
                Time = "13:51"
            },
            new ChatModel
            {
                ImageSource = "headshot.jpeg",
                Name = "Lucy",
                LastMessage = "Is your body from Mcdonals",
                Time = "13:51"
            }
        };
        }
    }
}