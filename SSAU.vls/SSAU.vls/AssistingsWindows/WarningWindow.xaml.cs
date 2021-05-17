using System.Windows;
using System.Windows.Input;

namespace SSAU.vls.AssistingsWindows
{
    /// <summary>
    /// Interaction logic for WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        public WarningWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Перемещение окна 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Okey_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
