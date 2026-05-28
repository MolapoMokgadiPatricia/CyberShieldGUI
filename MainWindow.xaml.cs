using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberShieldGUI
{
    // Code-behind for MainWindow.xaml.
    // Handles UI events, chat bubble creation, session management, and the typing delay.
    public partial class MainWindow : Window
    {
        private ChatBot? chatBot;

        // Holds a reference to the typing indicator bubble so it can be removed later
        private Border? typingIndicator;

        // Colour palette — white theme with indigo/purple accents
        private static readonly SolidColorBrush AccentBrush = new SolidColorBrush(Color.FromRgb(79, 70, 229)); // indigo
        private static readonly SolidColorBrush AccentLightBrush = new SolidColorBrush(Color.FromRgb(199, 210, 254)); // light indigo
        private static readonly SolidColorBrush BotBubbleBg = new SolidColorBrush(Color.FromRgb(243, 244, 246)); // light gray
        private static readonly SolidColorBrush BotBubbleBorder = new SolidColorBrush(Color.FromRgb(229, 231, 235)); // gray border
        private static readonly SolidColorBrush UserBubbleBg = new SolidColorBrush(Color.FromRgb(79, 70, 229)); // indigo
        private static readonly SolidColorBrush UserBubbleBorder = new SolidColorBrush(Color.FromRgb(67, 56, 202)); // darker indigo
        private static readonly SolidColorBrush TextDark = new SolidColorBrush(Color.FromRgb(17, 24, 39));  // near black
        private static readonly SolidColorBrush TextMuted = new SolidColorBrush(Color.FromRgb(107, 114, 128)); // gray
        private static readonly SolidColorBrush TextOnIndigo = new SolidColorBrush(Colors.White);

        public MainWindow()
        {
            InitializeComponent();
        }

        // Runs when the window first loads — plays the voice greeting and shows the ASCII logo
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AudioGreetings.PlayGreeting();
            DisplayAsciiLogo();
            NameInputBox.Focus();
        }

        // Adds the ASCII art logo to the top of the chat message area
        private void DisplayAsciiLogo()
        {
            string logo = @"

                     ▄████▄▓██   ██▓▄▄▄▄    ▓█████  ██▀███    ██████  ██░ ██  ██▓▓█████  ██▓    ▓█████▄
                   ▒██▀ ▀█ ▒██  ██▒▓█████▄  ▓█   ▀ ▓██ ▒ ██▒▒██    ▒ ▓██░ ██▒▓██▒▓█   ▀ ▓██▒    ▒██▀ ██▌
                   ▒▓█    ▄ ▒██ ██░▒██▒ ▄██ ▒███   ▓██ ░▄█ ▒░ ▓██▄   ▒██▀▀██░▒██▒▒███   ▒██░    ░██   █▌
                   ▒▓▓▄ ▄██▒░ ▐██▓░▒██░█▀   ▒▓█  ▄ ▒██▀▀█▄    ▒   ██▒░▓█ ░██ ░██░▒▓█  ▄ ▒██░    ░▓█▄   ▌
                   ▒ ▓███▀ ░░ ██▒▓░░▓█  ▀█▓▒░▒████▒░██▓ ▒██▒▒██████▒▒░▓█▒░██▓░██░░▒████▒░██████▒░▒████▓
                   ░ ░▒ ▒  ░ ██▒▒▒ ░▒▓███▀▒░░ ▒░ ░░ ▒▓ ░▒▓░▒ ▒▓▒ ▒ ░ ▒ ░░▒░▒░▓  ░░ ▒░ ░░ ▒░▓  ░ ▒▒▓  ▒
                     ░  ▒  ▓██ ░▒░ ▒░▒   ░  ░ ░  ░  ░▒ ░ ▒░░ ░▒  ░ ░ ▒ ░▒░ ░ ▒ ░ ░ ░  ░░ ░ ▒  ░ ░ ▒  ▒
                   ░       ▒ ▒ ░░   ░    ░    ░     ░░   ░ ░  ░  ░   ░  ░░ ░ ▒ ░   ░     ░ ░    ░ ░  ░
                   ░ ░     ░ ░      ░         ░  ░   ░           ░   ░  ░  ░ ░     ░  ░    ░  ░   ░   
                   ░       ░ ░           ░                                                        ░   
";

            // Wrap the logo in a coloured border panel
            var logoBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(238, 242, 255)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(199, 210, 254)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(16, 14, 16, 14),
                Margin = new Thickness(0, 0, 0, 20)
            };

            var logoPanel = new StackPanel();

            var logoText = new TextBlock
            {
                Text = logo,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 8,
                Foreground = AccentBrush,
                TextWrapping = TextWrapping.NoWrap,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var subtitleText = new TextBlock
            {
                Text = "Empowering South Africans to stay safe in the digital age",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 12,
                Foreground = TextMuted,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            };

            logoPanel.Children.Add(logoText);
            logoPanel.Children.Add(subtitleText);
            logoBorder.Child = logoPanel;
            MessagesPanel.Children.Add(logoBorder);
        }

        // Called when the user clicks "Start Session" or presses Enter in the name box
        private void StartSessionButton_Click(object sender, RoutedEventArgs e)
        {
            StartSession();
        }

        private void NameInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                StartSession();
        }

        // Initialises the chatbot, hides the name panel, and enables the chat input
        private void StartSession()
        {
            string name = NameInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
                name = "User";

            chatBot = new ChatBot(name);

            NameEntryPanel.Visibility = Visibility.Collapsed;
            MessageInputBox.IsEnabled = true;
            SendBtn.IsEnabled = true;
            UserNameLabel.Text = name;

            AddBotMessage(chatBot.GetWelcomeMessage());
            MessageInputBox.Focus();
        }

        // Called when the user clicks Send or presses Enter in the message box
        private void SendButton_Click(object sender, RoutedEventArgs e) => SendMessage();

        private void MessageInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        // Sends the user's message, shows a typing delay, then displays the bot's response
        private async void SendMessage()
        {
            string input = MessageInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            MessageInputBox.Text = string.Empty;
            AddUserMessage(input);

            SetInputEnabled(false);
            ShowTypingIndicator();

            await Task.Delay(550);

            RemoveTypingIndicator();

            string response = chatBot.ProcessInput(input);
            AddBotMessage(response);

            SetInputEnabled(true);
            MessageInputBox.Focus();
            UpdateSessionPanel();
        }

        // Sends the button's tag text as the user's message when a sidebar button is clicked
        private void QuickTopic_Click(object sender, RoutedEventArgs e)
        {
            if (chatBot == null) return;

            var btn = sender as Button;
            string query = btn?.Tag?.ToString();
            if (string.IsNullOrEmpty(query)) return;

            AddUserMessage(query);
            string response = chatBot.ProcessInput(query);
            AddBotMessage(response);
            UpdateSessionPanel();
        }

        // Adds a temporary "..." bubble while the bot is processing
        private void ShowTypingIndicator()
        {
            typingIndicator = BuildBubble("...", isUser: false, isTyping: true);
            MessagesPanel.Children.Add(typingIndicator);
            ScrollToBottom();
        }

        // Removes the typing indicator bubble
        private void RemoveTypingIndicator()
        {
            if (typingIndicator != null)
            {
                MessagesPanel.Children.Remove(typingIndicator);
                typingIndicator = null;
            }
        }

        private void AddUserMessage(string message)
        {
            MessagesPanel.Children.Add(BuildBubble(message, isUser: true, isTyping: false));
            ScrollToBottom();
        }

        private void AddBotMessage(string message)
        {
            MessagesPanel.Children.Add(BuildBubble(message, isUser: false, isTyping: false));
            ScrollToBottom();
        }

        // Builds a single chat bubble with a sender label.
        // Bot bubbles appear on the left (gray), user bubbles on the right (indigo).
        private Border BuildBubble(string message, bool isUser, bool isTyping)
        {
            // Sender label shown above the bubble
            var label = new TextBlock
            {
                Text = isUser ? "You" : "CyberShield",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 10,
                FontWeight = FontWeights.SemiBold,
                Foreground = isUser ? TextMuted : AccentBrush,
                Margin = new Thickness(isUser ? 0 : 48, 0, isUser ? 4 : 0, 4),
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            // Message text inside the bubble
            var content = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 13,
                Foreground = isUser ? TextOnIndigo : (isTyping ? TextMuted : TextDark),
                LineHeight = 22,
                FontStyle = isTyping ? FontStyles.Italic : FontStyles.Normal
            };

            // The bubble border itself
            var bubble = new Border
            {
                Background = isUser ? UserBubbleBg : BotBubbleBg,
                BorderBrush = isUser ? UserBubbleBorder : BotBubbleBorder,
                BorderThickness = new Thickness(1),
                CornerRadius = isUser
                                    ? new CornerRadius(16, 4, 16, 16)
                                    : new CornerRadius(4, 16, 16, 16),
                Padding = new Thickness(14, 10, 14, 10),
                MaxWidth = 600,
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                Child = content
            };

            // For bot messages, place a small avatar circle to the left of the bubble
            FrameworkElement bubbleRow;

            if (!isUser)
            {
                var avatar = new Border
                {
                    Width = 34,
                    Height = 34,
                    CornerRadius = new CornerRadius(17),
                    Background = AccentBrush,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 8, 0),
                    Child = new TextBlock
                    {
                        Text = "CS",
                        FontSize = 11,
                        FontWeight = FontWeights.Bold,
                        Foreground = TextOnIndigo,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                };

                var row = new StackPanel { Orientation = Orientation.Horizontal };
                row.Children.Add(avatar);
                row.Children.Add(bubble);
                bubbleRow = row;
            }
            else
            {
                bubbleRow = bubble;
            }

            // Wrap the label and bubble in a stack panel for spacing
            var wrapper = new StackPanel
            {
                Margin = isUser
                    ? new Thickness(80, 6, 0, 6)
                    : new Thickness(0, 6, 80, 6),
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            wrapper.Children.Add(label);
            wrapper.Children.Add(bubbleRow);
            return new Border { Child = wrapper };
        }

        // Scrolls the chat area to the latest message
        private void ScrollToBottom()
        {
            ChatScrollViewer.UpdateLayout();
            ChatScrollViewer.ScrollToEnd();
        }

        private void SetInputEnabled(bool enabled)
        {
            MessageInputBox.IsEnabled = enabled;
            SendBtn.IsEnabled = enabled;
        }

        // Updates the sidebar session panel with the user's stored interest topic
        private void UpdateSessionPanel()
        {
            string interest = chatBot?.GetUserInterest();
            if (!string.IsNullOrEmpty(interest))
            {
                InterestLabel.Text = $"Interested in: {interest}";
                InterestLabel.Foreground = AccentBrush;
            }
            else
            {
                InterestLabel.Text = "No interest stored yet.";
                InterestLabel.Foreground = TextMuted;
            }
        }
    }
}