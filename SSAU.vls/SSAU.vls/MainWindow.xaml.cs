using System.Windows;
using SSAU.vls.FIRST.lr;
using System.Windows.Input;
using SSAU.vls.AssistingsWindows;

namespace SSAU.vls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Заголовок окна потверждения выхода
        /// </summary>
        private string _nameLabelExit = "Вы действительно хотите выйти из приложения?";

        public MainWindow()
        {
            InitializeComponent();
        }
     
        /// <summary>
        /// Кнопка, вызывающая окно потверждения для закрытия приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var closeWindow = new CloseWindow();
            closeWindow.TextExit.Text = this._nameLabelExit;
            closeWindow.ShowDialog();

            if (closeWindow.con == true)
            {
                this.Close();
            } else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Сворачивание окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized; 
        }

        /// <summary>
        /// Перетаскивание основного окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBarMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {    
                this.DragMove();
            }
        }

        /// <summary>
        /// Открытие окна первой лр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window window = new FirstLrWindow();
            window.Show();
            this.Close();
        }
    }
}
