using SSAU.vls.AssistingsWindows;
using SSAU.vls.FIRST.lr.Calculation.Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SSAU.vls.FIRST.lr.ExportToExcel.Models;
using System.Collections.Generic;

namespace SSAU.vls.FIRST.lr
{
    /// <summary>
    /// Interaction logic for FirstLrWindow.xaml
    /// </summary>
    public partial class FirstLrWindow : Window
    {
        /// <summary>
        /// Заголовок окна потверждения выхода
        /// </summary>
        private string _nameLabelExit = "Вы действительно хотите выйти?\n Ваш прогресс не будет сохранен";

        /// <summary>
        /// Заголовок окна предупреждения
        /// </summary>
        private string _textWarningWindow = "Запустите таймер!";

        /// <summary>
        /// Начальное значение отсчета таймера (Наработка)
        /// </summary>
        private int _startValue = 1;

        /// <summary>
        /// Счетчик
        /// </summary>
        private int count = 2;

        /// <summary>
        /// Модель для расчета
        /// </summary>
        public CalculationModel model;

        public Random random;

        /// <summary>
        /// Результат отказа
        /// </summary>
        public double result;

        /// <summary>
        /// Таймер
        /// </summary>
        private DispatcherTimer timer;

        public double gradualFailure;
        public double suddenFailure;

        public FirstLrWindow()
        {
            InitializeComponent();
            model = new CalculationModel();
            random = new Random();

        }

        /// <summary>
        /// Перетаскивание окна
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
        /// Сворачивание окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Закрытие окна
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
                Window window = new MainWindow();
                window.Show();
                this.Close();
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }
        
        /// <summary>
        /// Внезапные отказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sudden_Checked(object sender, RoutedEventArgs e)
        {
            Gradual.IsChecked = false;
            result = random.NextDouble() * suddenFailure;
        }

        /// <summary>
        /// Постепенные отказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gradual_Checked(object sender, RoutedEventArgs e)
        {
            Sudden.IsChecked = false;
            result = random.NextDouble() * gradualFailure;
        }

        /// <summary>
        /// Запуск лабораторной работы 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region
            Uri resourceStartUri = new Uri("/Resources/Images/start_active.png", UriKind.Relative);
            Start.Source = new BitmapImage(resourceStartUri);

            Uri resourcePauseUri = new Uri("/Resources/Images/pause.png", UriKind.Relative);
            Pause.Source = new BitmapImage(resourcePauseUri);

            Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
            Stop.Source = new BitmapImage(resourceStopUri);
            #endregion
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Start();
        }

        /// <summary>
        /// Остановка лабораторной работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pause_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region
            Uri resourcePauseUri = new Uri("/Resources/Images/pause_active.png", UriKind.Relative);
            Pause.Source = new BitmapImage(resourcePauseUri);

            Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
            Start.Source = new BitmapImage(resourceStartUri);

            Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
            Stop.Source = new BitmapImage(resourceStopUri);
            #endregion 

            if(timer == null)
            {
                WarningWindow warWindow = new WarningWindow();
                warWindow.TextExit.Text = this._textWarningWindow;
                warWindow.ShowDialog();
                Uri resourcePause = new Uri("/Resources/Images/pause.png", UriKind.Relative);
                Pause.Source = new BitmapImage(resourcePause);
                e.Handled = true;
                
            } else
            {
                timer.Stop();
            }
        }

        /// <summary>
        /// Сбрасывание таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region
            Uri resourceStopUri = new Uri("/Resources/Images/stop_active.png", UriKind.Relative);
            Stop.Source = new BitmapImage(resourceStopUri);

            Uri resourcePauseUri = new Uri("/Resources/Images/pause.png", UriKind.Relative);
            Pause.Source = new BitmapImage(resourcePauseUri);

            Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
            Start.Source = new BitmapImage(resourceStartUri);
            #endregion

            if(timer == null)
            {
                WarningWindow warWindow = new WarningWindow();
                warWindow.TextExit.Text = this._textWarningWindow;
                warWindow.ShowDialog();
                Uri resourceStop = new Uri("/Resources/Images/stop.png", UriKind.Relative);
                Stop.Source = new BitmapImage(resourceStop);
                e.Handled = true;
            } else
            {
                timer.Stop();
                this._startValue = 0;
                TimerOutput.Content = ToStringTimeFormat(_startValue);
            }
        }

        /// <summary>
        /// Обработка события таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTick(object sender, EventArgs e)
        {
            gradualFailure = (1 - Math.Exp(-model.Lambda * model.Time)) * model.Power;
            suddenFailure = (1 - Math.Exp(-model.Lambda * model.Time)) * model.Power * Calculation.Calculation.k4 * Calculation.Calculation.k5 * Calculation.Calculation.k6 * Calculation.Calculation.k7 * Calculation.Calculation.k8;
            int TimeSliderValue = (int)TimeSlider.Value;
            if (CheckBox.IsChecked == true)
            {
                timer.Stop();
            }
            CheckBox.IsChecked = false;
            if (TimeSlider.Value == 0)
            {
                TimerOutput.Content = ToStringTimeFormat(_startValue);
            }
            else
            {
                TimerOutput.Content = ToStringTimeFormat(_startValue * TimeSliderValue);
                model.Time = (_startValue * TimeSliderValue);
                _startValue++;
            }
            if (TimeSliderValue * _startValue >= 9999)
            {
                _startValue = 9999;
                TimerOutput.Content = ToStringTimeFormat(_startValue);
                timer.Stop();
            }

            var calculationFailure = Calculation.Calculation.CalculationSuddenFailure(model, count);
            var calculationComModel = Calculation.Calculation.CalculationComModel(model);
            var calculationTypes = Calculation.Calculation.CalculationTypes(calculationFailure);
            CheckFailure(calculationTypes, calculationComModel);

            ComModel.ExportList.Add(new ComModel() { Com_1 = "1 лр", Com_2 = 0, Com_3 = model.Power, Com_4 = Math.Round(calculationComModel.Com_4, 3), Com_5 = Math.Round(calculationComModel.Com_5, 3), Com_6 = Math.Round(calculationComModel.Com_6, 3), Com_7 = Math.Round(calculationComModel.Com_7, 3), Com_8 = Math.Round(calculationComModel.Com_8, 3) });
        }

        /// <summary>
        /// Возвращает строку - в формате время
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string ToStringTimeFormat(int time)
        {
            if (_startValue == 0)
            {
                return String.Format("{0}", _startValue);
            }
            return String.Format("{0}", time);
        }

        /// <summary>
        /// Перемещение ползунка временного слайда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        /// <summary>
        /// Перемещение позунка слайда мощности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.Power = (int)PowerSlider.Value;
        }

        /// <summary>
        /// Завершение лаб. работы 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textEndLR = "Завершить лабораторную работу?";
            var windowEnd = new CloseWindow();
            windowEnd.TextExit.Text = textEndLR;

            windowEnd.ShowDialog();
            if (windowEnd.con == true)
            {
                var loginWindow = new LoginWindow(ComModel.ExportList);
                loginWindow.ShowDialog();
                this.Close();
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        #region Lambda
        private void Lambda001_Click(object sender, RoutedEventArgs e)
        {
            Menu.Header = "0.001";
            model.Lambda = 0.001;
        }

        private void Lambda0001_Click(object sender, RoutedEventArgs e)
        {
            Menu.Header = "0.0001";
            model.Lambda = 0.0001;
        }

        private void Lambda00001_Click(object sender, RoutedEventArgs e)
        {
            Menu.Header = "0.00001";
            model.Lambda = 0.00001;
        }

        private void Lambda000001_Click(object sender, RoutedEventArgs e)
        {
            Menu.Header = "0.000001";
            model.Lambda = 0.000001;

        }
        #endregion

        /// <summary>
        /// Кнопка при возникновении отказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox.IsChecked = false;
        }

        /// <summary>
        /// Проверка на отказы
        /// </summary>
        public void CheckFailure(TypeModel type, ComModel com)
        {
            if (type.Type15 < suddenFailure || type.Type15 < gradualFailure)
            {
                CheckBox.IsChecked = true;
                #region
                Uri resourcePauseUri = new Uri("/Resources/Images/pause_active.png", UriKind.Relative);
                Pause.Source = new BitmapImage(resourcePauseUri);

                Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
                Start.Source = new BitmapImage(resourceStartUri);

                Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
                Stop.Source = new BitmapImage(resourceStopUri);
                #endregion

                var validFailure = 15;
                
                ComModel.ExportList.Add(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
                SendToController.SendToController.Send(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
            }
            else if (type.Type17 < suddenFailure || type.Type17 < gradualFailure)
            {
                CheckBox.IsChecked = true;
                #region
                Uri resourcePauseUri = new Uri("/Resources/Images/pause_active.png", UriKind.Relative);
                Pause.Source = new BitmapImage(resourcePauseUri);

                Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
                Start.Source = new BitmapImage(resourceStartUri);

                Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
                Stop.Source = new BitmapImage(resourceStopUri);
                #endregion

                var validFailure = 17;
                ComModel.ExportList.Add(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
                SendToController.SendToController.Send(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
            }
            else if (type.Type30 < suddenFailure || type.Type30 < gradualFailure)
            {
                CheckBox.IsChecked = true;
                #region
                Uri resourcePauseUri = new Uri("/Resources/Images/pause_active.png", UriKind.Relative);
                Pause.Source = new BitmapImage(resourcePauseUri);

                Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
                Start.Source = new BitmapImage(resourceStartUri);

                Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
                Stop.Source = new BitmapImage(resourceStopUri);
                #endregion

                var validFailure = 30;
                ComModel.ExportList.Add(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
                SendToController.SendToController.Send(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
            }
            else if (type.Type50 < suddenFailure || type.Type50 < gradualFailure)
            {
                CheckBox.IsChecked = true;
                #region
                Uri resourcePauseUri = new Uri("/Resources/Images/pause_active.png", UriKind.Relative);
                Pause.Source = new BitmapImage(resourcePauseUri);

                Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
                Start.Source = new BitmapImage(resourceStartUri);

                Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
                Stop.Source = new BitmapImage(resourceStopUri);
                #endregion

                var validFailure = 50;
                ComModel.ExportList.Add(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
                SendToController.SendToController.Send(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
            }
            else if (type.Type80 < suddenFailure || type.Type80 < gradualFailure)
            {
                CheckBox.IsChecked = true;
                #region
                Uri resourcePauseUri = new Uri("/Resources/Images/pause_active.png", UriKind.Relative);
                Pause.Source = new BitmapImage(resourcePauseUri);

                Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
                Start.Source = new BitmapImage(resourceStartUri);

                Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
                Stop.Source = new BitmapImage(resourceStopUri);
                #endregion

                var validFailure = 80;
                ComModel.ExportList.Add(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
                SendToController.SendToController.Send(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
            }
            else if (type.Type200 < suddenFailure || type.Type200 < gradualFailure)
            {
                CheckBox.IsChecked = true;
                #region
                Uri resourcePauseUri = new Uri("/Resources/Images/pause_active.png", UriKind.Relative);
                Pause.Source = new BitmapImage(resourcePauseUri);

                Uri resourceStartUri = new Uri("/Resources/Images/start.png", UriKind.Relative);
                Start.Source = new BitmapImage(resourceStartUri);

                Uri resourceStopUri = new Uri("/Resources/Images/stop.png", UriKind.Relative);
                Stop.Source = new BitmapImage(resourceStopUri);
                #endregion

                var validFailure = 200;
                ComModel.ExportList.Add(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
                SendToController.SendToController.Send(new ComModel() { Com_1 = "1 лр", Com_2 = validFailure, Com_3 = model.Power, Com_4 = Math.Round(com.Com_4, 3), Com_5 = Math.Round(com.Com_5, 3), Com_6 = Math.Round(com.Com_6), Com_7 = Math.Round(com.Com_7), Com_8 = Math.Round(com.Com_8) });
            }
        }
    }
}
