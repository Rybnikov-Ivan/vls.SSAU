using SSAU.vls.AssistingsWindows;
using SSAU.vls.FIRST.lr.Calculation.Models;
using SSAU.vls.FIRST.lr.ExportToExcel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SSAU.vls.FIRST.lr.Calculation;
using System.Threading.Tasks;
using System.Threading;

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

        private int count = 2;
        /// <summary>
        /// Модель для расчета
        /// </summary>
        public CalculationModel model;

        /// <summary>
        /// Таймер
        /// </summary>
        private DispatcherTimer timer;

        CancellationTokenSource tokenSource;


        public FirstLrWindow()
        {
            InitializeComponent();
            model = new CalculationModel();
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

        }

        /// <summary>
        /// Постепенные отказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gradual_Checked(object sender, RoutedEventArgs e)
        {

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
            tokenSource = new CancellationTokenSource();
            CancellationToken cancelToken = tokenSource.Token;
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
                tokenSource.Cancel();
                timer.Stop();
            }
        }

        /// <summary>
        /// Сбрасывание тайме
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
                tokenSource.Cancel();
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
            int TimeSliderValue = (int)TimeSlider.Value;
            
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
            Task.Run(() => Calculation.Calculation.CalculationSuddenFailure(model, count))
                .ContinueWith(task =>
                {

                    count++;
                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
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
                Window window = new MainWindow();
                window.Show();
                tokenSource.Cancel();
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
    }
}
