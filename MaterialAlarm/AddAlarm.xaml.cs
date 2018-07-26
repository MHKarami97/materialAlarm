using System.Windows;
using System.Windows.Input;

namespace MaterialAlarm
{
    /// <summary>
    /// Interaction logic for AddAlarm.xaml
    /// </summary>
    public partial class AddAlarm : Window
    {
        public AddAlarm()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddNewAlarm(object sender, MouseButtonEventArgs e)
        {
            PopUpDock.Visibility = Visibility.Visible;
            POpUp.IsOpen = true;
        }
    }
}
