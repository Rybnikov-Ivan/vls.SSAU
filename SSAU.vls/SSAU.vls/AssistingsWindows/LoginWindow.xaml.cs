using SSAU.vls.AssistingsWindows.Models;
using SSAU.vls.FIRST.lr.ExportToExcel;
using SSAU.vls.FIRST.lr.ExportToExcel.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace SSAU.vls.AssistingsWindows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<ComModel> _comModels;
        public LoginWindow(List<ComModel> comModels)
        {
            InitializeComponent();

            this._comModels = comModels;
        }

        /// <summary>
        /// Перемещение окна по ToolBar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Переход на главное окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var resultFile = new ExportExcel();
            resultFile.SaveEndOfBase(this._comModels, LastName.Text, Group.Text);
            Window window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
