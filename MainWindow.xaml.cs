using Poliklinika20.DB;
using Poliklinika20.Pages;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Poliklinika20
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer _TimerCalc;
        public DateTime date;
        public users user;
        public int _TimerSeconds = 60; //60
        private int CDmins = 1;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Auth();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        { 
            if (user!=null)
            BackBtn.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;

        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            _TimerSeconds--;

            // Обновление текста таймера
            TimerBlock.Text = $"Осталось времени: {FormatTime(_TimerSeconds)}";

            // Проверка времени для предупреждения (2:30)
            if (_TimerSeconds == 30)
            {
                MessageBox.Show("Ваш сеанс завершится через 30 секунд.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Проверка времени для выхода (3:00)
            if (_TimerSeconds <= 0)
            {
                _TimerCalc.Stop(); // Остановка таймера
                LogoutUser(); // Выход из аккаунта
            }
        }
        private string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            return $"{minutes:D2}:{remainingSeconds:D2}";
        }

        public void StartSessionTimer()
        {
            // Инициализация таймера
            _TimerCalc = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // Интервал обновления: 1 секунда
            };

            // Подписка на событие Tick
            _TimerCalc.Tick += SessionTimer_Tick;

            // Запуск таймера
            _TimerCalc.Start();
        }

        private void LogoutUser()
        {
            user = null;

            if (_TimerCalc != null)
            {
                _TimerCalc.Stop();
                _TimerCalc = null;
            }
            MainFrame.Content = new Auth();

            _TimerSeconds = 60;
            TimerBlock.Text = "";
        }
    }

    }
