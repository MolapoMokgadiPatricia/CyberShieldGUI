using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CyberShieldGUI
{
    public partial class MainWindow : Window
    {
        private ChatBot? chatBot;
        private Border? typingIndicator;

        // ── Colours matching the image ────────────────────────────────────────────
        // User bubble  — solid blue (matches "Shipping" pill in the image)
        private static readonly SolidColorBrush UserBubbleBg = new SolidColorBrush(Color.FromRgb(29, 78, 216));  // #1D4ED8
        private static readonly SolidColorBrush UserBubbleBorder = new SolidColorBrush(Color.FromRgb(29, 78, 216));

        // Bot bubble   — white card with light border (matches left bubbles in the image)
        private static readonly SolidColorBrush BotBubbleBg = new SolidColorBrush(Colors.White);
        private static readonly SolidColorBrush BotBubbleBorder = new SolidColorBrush(Color.FromRgb(226, 232, 240));  // #E2E8F0

        // Text colours
        private static readonly SolidColorBrush TextDark = new SolidColorBrush(Color.FromRgb(15, 23, 42));   // #0F172A
        private static readonly SolidColorBrush TextMuted = new SolidColorBrush(Color.FromRgb(100, 116, 139));  // #64748B
        private static readonly SolidColorBrush TextWhite = new SolidColorBrush(Colors.White);
        private static readonly SolidColorBrush AccentBlue = new SolidColorBrush(Color.FromRgb(29, 78, 216));  // #1D4ED8

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AudioGreetings.PlayGreeting();
            DisplayAsciiLogo();
            NameInputBox.Focus();
        }

        // Shows the ASCII logo inside a styled card at the top of the chat area
        private void DisplayAsciiLogo()
        {
            string logo = @"
  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗██╗  ██╗██╗███████╗██╗     ██████╗ 
 ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██║  ██║██║██╔════╝██║     ██╔══██╗
 ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗███████║██║█████╗  ██║     ██║  ██║
 ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══██║██║██╔══╝  ██║     ██║  ██║
 ╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║██║  ██║██║███████╗███████╗██████╔╝
  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝╚══════╝╚══════╝╚═════╝";

            // Outer card with subtle blue tint
            var card = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(239, 246, 255)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(191, 219, 254)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(16),
                Padding = new Thickness(18, 16, 18, 16),
                Margin = new Thickness(0, 0, 0, 24)
            };

            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = logo,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 7.5,
                Foreground = AccentBlue,
                TextWrapping = TextWrapping.NoWrap,
                HorizontalAlignment = HorizontalAlignment.Center
            });

            panel.Children.Add(new TextBlock
            {
                Text = "Empowering South Africans to stay safe in the digital age ",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 12,
                Foreground = TextMuted,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            });

            card.Child = panel;
            MessagesPanel.Children.Add(card);
        }

        // ── Session ───────────────────────────────────────────────────────────────

        private void StartSessionButton_Click(object sender, RoutedEventArgs e) => StartSession();

        private void NameInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StartSession();
        }

        private void StartSession()
        {
            string name = NameInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) name = "User";

            chatBot = new ChatBot(name);

            NameEntryPanel.Visibility = Visibility.Collapsed;
            MessageInputBox.IsEnabled = true;
            SendBtn.IsEnabled = true;
            UserNameLabel.Text = $"👤 {name}";
            StatusLabel.Text = "We're online";

            AddBotMessage(chatBot.GetWelcomeMessage());
            MessageInputBox.Focus();
        }

        // ── Sending ───────────────────────────────────────────────────────────────

        private void SendButton_Click(object sender, RoutedEventArgs e) => SendMessage();

        private void MessageInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SendMessage();
        }

        private async void SendMessage()
        {
            string input = MessageInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            MessageInputBox.Text = string.Empty;
            AddUserMessage(input);

            SetInputEnabled(false);
            ShowTypingIndicator();

            await Task.Delay(600);

            RemoveTypingIndicator();

            string response = chatBot!.ProcessInput(input);
            AddBotMessage(response);

            SetInputEnabled(true);
            MessageInputBox.Focus();
            UpdateSessionPanel();
        }

        // Quick-reply pill buttons
        private void QuickTopic_Click(object sender, RoutedEventArgs e)
        {
            if (chatBot == null) return;

            var btn = sender as Button;
            string? query = btn?.Tag?.ToString();
            if (string.IsNullOrEmpty(query)) return;

            AddUserMessage(query);
            string response = chatBot.ProcessInput(query);
            AddBotMessage(response);
            UpdateSessionPanel();
        }

        // ── Typing indicator ──────────────────────────────────────────────────────

        private void ShowTypingIndicator()
        {
            typingIndicator = BuildBotBubble("typing");
            MessagesPanel.Children.Add(typingIndicator);
            ScrollToBottom();
        }

        private void RemoveTypingIndicator()
        {
            if (typingIndicator != null)
            {
                MessagesPanel.Children.Remove(typingIndicator);
                typingIndicator = null;
            }
        }

        // ── Message builders ──────────────────────────────────────────────────────

        private void AddUserMessage(string message)
        {
            MessagesPanel.Children.Add(BuildUserBubble(message));
            ScrollToBottom();
        }

        private void AddBotMessage(string message)
        {
            MessagesPanel.Children.Add(BuildBotBubble(message));
            ScrollToBottom();
        }

        // User bubble — blue, right-aligned, no avatar (matches image)
        private FrameworkElement BuildUserBubble(string message)
        {
            var bubble = new Border
            {
                Background = UserBubbleBg,
                BorderBrush = UserBubbleBorder,
                BorderThickness = new Thickness(0),
                CornerRadius = new CornerRadius(18, 18, 4, 18),
                Padding = new Thickness(16, 10, 16, 10),
                MaxWidth = 520,
                Child = new TextBlock
                {
                    Text = message,
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("Segoe UI"),
                    FontSize = 13,
                    Foreground = TextWhite,
                    LineHeight = 22
                }
            };

            bubble.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Color.FromRgb(29, 78, 216),
                BlurRadius = 8,
                Opacity = 0.2,
                ShadowDepth = 2
            };

            var wrapper = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(80, 4, 0, 4)
            };

            // "You" label
            wrapper.Children.Add(new TextBlock
            {
                Text = "You",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 10,
                Foreground = TextMuted,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 4, 4)
            });

            wrapper.Children.Add(bubble);
            return wrapper;
        }

        // Bot bubble — white card with avatar circle, left-aligned (matches image)
        private Border BuildBotBubble(string message)
        {
            bool isTyping = message == "typing";

            // Avatar circle (like the profile image in the header)
            var avatar = new Border
            {
                Width = 38,
                Height = 38,
                CornerRadius = new CornerRadius(19),
                BorderBrush = new SolidColorBrush(Color.FromRgb(191, 219, 254)),
                BorderThickness = new Thickness(2),
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 10, 0)
            };

            avatar.Background = new LinearGradientBrush(
                Color.FromRgb(96, 165, 250),
                Color.FromRgb(37, 99, 235),
                new Point(0, 0), new Point(1, 1));

            avatar.Child = new TextBlock
            {
                Text = "CS",
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Foreground = TextWhite,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Message content
            var bubbleBorder = new Border
            {
                Background = BotBubbleBg,
                BorderBrush = BotBubbleBorder,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4, 18, 18, 18),
                Padding = new Thickness(16, 10, 16, 10),
                MaxWidth = 520,
                Child = new TextBlock
                {
                    Text = isTyping ? "● ● ●" : message,
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("Segoe UI"),
                    FontSize = isTyping ? 16 : 13,
                    Foreground = isTyping ? TextMuted : TextDark,
                    LineHeight = isTyping ? 24 : 22,
                    FontStyle = FontStyles.Normal
                }
            };

            bubbleBorder.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Colors.Black,
                BlurRadius = 6,
                Opacity = 0.06,
                ShadowDepth = 2
            };

            // Avatar + bubble row
            var row = new StackPanel { Orientation = Orientation.Horizontal };
            row.Children.Add(avatar);
            row.Children.Add(bubbleBorder);

            // Label + row wrapper
            var labelRow = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 4, 80, 4)
            };

            labelRow.Children.Add(new TextBlock
            {
                Text = "CyberShield",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 10,
                Foreground = AccentBlue,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(50, 0, 0, 4)
            });

            labelRow.Children.Add(row);

            return new Border { Child = labelRow };
        }

        // ── Utilities ─────────────────────────────────────────────────────────────

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

        private void UpdateSessionPanel()
        {
            if (chatBot == null) return;

            string? interest = chatBot.GetUserInterest();
            if (!string.IsNullOrEmpty(interest))
            {
                InterestLabel.Text = $"Interested in: {interest}";
                InterestLabel.Foreground = AccentBlue;
            }
            else
            {
                InterestLabel.Text = "No interest stored yet.";
                InterestLabel.Foreground = TextMuted;
            }

            int logCount = chatBot.GetActivityLog().Count;
            LogCountLabel.Text = $"Log entries: {logCount}";
            LogCountLabel.Foreground = logCount > 0 ? AccentBlue : TextMuted;
        }
    }
}

