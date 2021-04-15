using System.Windows;
using System.Windows.Input;

namespace SSAU.vls.AssistingsWindows
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class CloseWindow : Window
    {
        /// <summary>
        /// Состояние потверждения закрытия
        /// </summary>
        public bool con;

        public CloseWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Клик закрывает приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseYes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            con = true;
        }

        /// <summary>
        /// Клик продолжает работу приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            con = false;
        }

        /// <summary>
        /// Перемещение окна потверждения
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
    }
}
