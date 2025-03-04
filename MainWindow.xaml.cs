using Poliklinika20.DB;
using Poliklinika20.Pages;
using System;
using System.Windows;

namespace Poliklinika20
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DateTime date;
        public  users user;

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
            BackBtn.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;

        }
    }
}
