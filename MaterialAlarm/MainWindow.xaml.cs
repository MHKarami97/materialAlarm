using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using ToastNotifications.Core;

namespace MaterialAlarm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _vm = new ToastViewModel();
        }
        private readonly ToastViewModel _vm;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key?.SetValue("MaterailAlarm", "\"" + System.Windows.Forms.Application.ExecutablePath + "\"");
            }

            GetIcon1().Icon = Properties.Resources.taskbarIcon;
            GetIcon1().Visible = true;
        }

        string _lastMessage;
        void ShowMessage(Action<string, MessageOptions> action, string name)
        {
            MessageOptions opts = new MessageOptions
            {
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true,
                ShowCloseButton = true
            };
            _lastMessage = $"{name}";
            action(_lastMessage, opts);
        }

        NotifyIcon _icon = new NotifyIcon();

        public NotifyIcon GetIcon1()
        {
            return _icon;
        }

        private void CloseBtn(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ShowSite(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://itarfand.com");
        }

        private void MiniBtn(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SelecAllAlarm(object sender, RoutedEventArgs e)
        {

        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private string _listStatus = "hideList";
        private void SHowListBtn(object sender, RoutedEventArgs e)
        {
            if (_listStatus.Equals("hideList"))
            {
                LeftList.Visibility = Visibility.Visible;

                TranslateTransform trans = new TranslateTransform();
                DoubleAnimation anim = new DoubleAnimation(1, 210, TimeSpan.FromSeconds(0.5));
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
                LeftList.RenderTransform = trans;

                var bc = new BrushConverter();
                MainCanvas.Background = (Brush)bc.ConvertFrom("#60646D");
                MainCanvas.Opacity = 0.4;

                _listStatus = "visibleList";
            }
            else if (_listStatus.Equals("visibleList"))
            {
                TranslateTransform trans = new TranslateTransform();
                DoubleAnimation anim = new DoubleAnimation(210, 1, TimeSpan.FromSeconds(0.5));
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
                LeftList.RenderTransform = trans;

                var bc = new BrushConverter();
                MainCanvas.Background = (Brush)bc.ConvertFrom("#fff");
                MainCanvas.Opacity = 0;

                _listStatus = "hideList";
            }
        }

        private void ColorChangeToggleButton(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            switch (ChangeColor.IsChecked)
            {
                case true:
                    MainGrid.Background = (Brush)bc.ConvertFrom("#303030");
                    TopMenu.Background = (Brush)bc.ConvertFrom("#607D8B");  
                    TopRectangle.Fill = (Brush)bc.ConvertFrom("#607D8B");  
                    ShowMessage(_vm.ShowSuccess, "Dark Theme");
                    
                    break;
                case false:
                    MainGrid.Background = (Brush)bc.ConvertFrom("#FAFAFA");
                    TopMenu.Background = (Brush)bc.ConvertFrom("#512DA8");
                    TopRectangle.Fill = (Brush)bc.ConvertFrom("#512DA8");  
                    ShowMessage(_vm.ShowInformation, "Light Theme");
                    break;
            }
        }

        private void AddAlarmBtn(object sender, RoutedEventArgs e)
        {
            new AddAlarm().Show();
        }
    }
}
