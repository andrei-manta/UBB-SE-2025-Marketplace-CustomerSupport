using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.UI.Xaml.Media.Imaging;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Marketplace_SE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatPage : Page
    {
        private bool isConnected = false;
        private const int sendPort = 13000;
        private const int listenPort = 13001;
        private List<string> chatHistory = new();

        public ChatPage()
        {
            this.InitializeComponent();
        }


        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnected)
            {
                isConnected = true;
                StartListening();
                AddMessageToChat("Connected and listening on port " + listenPort, false);
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Text.Trim();
            if (string.IsNullOrEmpty(message)) return;

            SendMessage(message);
            AddMessageToChat(message, true);
            MessageBox.Text = "";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainMarketplacePage));
        }

        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Text File", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "ChatHistory";

            // Required for WinUI 3 desktop apps
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hWnd);

            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                await Windows.Storage.FileIO.WriteLinesAsync(file, chatHistory);
            }
        }


        private void AddMessageToChat(string message, bool isClient)
        {
            string timeStamp = DateTime.Now.ToString("[HH:mm]");
            string displayText = $"{timeStamp} {message}";

            var textBlock = new TextBlock
            {
                Text = displayText,
                TextWrapping = TextWrapping.WrapWholeWords,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White)
            };

            var border = new Border
            {
                Child = textBlock,
                Background = new SolidColorBrush(isClient ? Microsoft.UI.Colors.Green : Microsoft.UI.Colors.Red),
                CornerRadius = new CornerRadius(6),
                Margin = new Thickness(0, 4, 0, 4),
                Padding = new Thickness(8),
                HorizontalAlignment = isClient ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                MaxWidth = 250
            };

            ChatPanel.Children.Add(border);

            string prefix = isClient ? "[You]" : "[Peer]";
            chatHistory.Add($"{timeStamp} {prefix}: {message}");

            // Auto-scroll to bottom
            if (ChatPanel.Parent is ScrollViewer scrollViewer)
            {
                scrollViewer.ChangeView(null, scrollViewer.ScrollableHeight, null);
            }
        }



        private void StartListening()
        {
            Thread listenerThread = new Thread(() =>
            {
                TcpListener listener = new TcpListener(IPAddress.Any, listenPort);
                listener.Start();

                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        NetworkStream stream = client.GetStream();
                        byte[] buffer = new byte[1024 * 1024]; // 1MB buffer to support image files
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        // Detect if message is an image
                        bool isImage = bytesRead > 4 &&
                                       buffer[0] == (byte)'I' &&
                                       buffer[1] == (byte)'M' &&
                                       buffer[2] == (byte)'G' &&
                                       buffer[3] == (byte)'|';

                        if (isImage)
                        {
                            byte[] imageBytes = buffer.Skip(4).Take(bytesRead - 4).ToArray();

                            DispatcherQueue.TryEnqueue(() =>
                            {
                                DisplayImageFromBytes(imageBytes, isClient: false);
                                chatHistory.Add("[Peer]: <received image>");
                            });
                        }
                        else
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            DispatcherQueue.TryEnqueue(() =>
                            {
                                AddMessageToChat(message, isClient: false);
                            });
                        }

                        client.Close();
                    }
                    catch (Exception ex)
                    {
                        DispatcherQueue.TryEnqueue(() =>
                        {
                            AddMessageToChat("Receive error: " + ex.Message, false);
                        });
                    }
                }
            });

            listenerThread.IsBackground = true;
            listenerThread.Start();
        }


        private void SendMessage(string message)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", sendPort); // Send to server
                byte[] data = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    AddMessageToChat("Send failed: " + ex.Message, false);
                });
            }
        }

        private async void AttachButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var buffer = await Windows.Storage.FileIO.ReadBufferAsync(file);
                byte[] bytes = buffer.ToArray();

                // Tag the image so receiver knows what it is
                byte[] header = Encoding.UTF8.GetBytes("IMG|");
                byte[] fullMessage = header.Concat(bytes).ToArray();

                SendBytes(fullMessage);
                DisplayImageFromBytes(bytes, isClient: true);
                chatHistory.Add($"[You]: <sent image>");
            }
        }

        private void SendBytes(byte[] data)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", sendPort);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    AddMessageToChat("Send failed: " + ex.Message, false);
                });
            }
        }

        private void DisplayImageFromBytes(byte[] imageData, bool isClient)
        {
            using var stream = new MemoryStream(imageData);
            var bitmap = new BitmapImage();
            stream.Position = 0;

            bitmap.SetSource(stream.AsRandomAccessStream());

            var image = new Image
            {
                Source = bitmap,
                Width = 150,
                Height = 150,
                Stretch = Stretch.UniformToFill
            };

            var border = new Border
            {
                Child = image,
                Background = new SolidColorBrush(isClient ? Microsoft.UI.Colors.Green : Microsoft.UI.Colors.Red),
                CornerRadius = new CornerRadius(6),
                Margin = new Thickness(0, 4, 0, 4),
                Padding = new Thickness(4),
                HorizontalAlignment = isClient ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            ChatPanel.Children.Add(border);
        }

    }
}
