
using Microsoft.Win32;
using Poliklinika20.DB;
using Poliklinika20.Pages.Lists;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Poliklinika20.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            DataContext = ((MainWindow)Application.Current.MainWindow).user;

            switch (((MainWindow)Application.Current.MainWindow).user.id_role)
            {
                case 1:
                    {
                       


                        AllPatients.Visibility = Visibility.Collapsed;
                            AllServices.Visibility = Visibility.Collapsed;
                        AllOrders.Visibility = Visibility.Collapsed;
                        ReportBtn.Visibility = Visibility.Visible;
                        LogBtn.Visibility = Visibility.Collapsed;
                        analyzerBtn.Visibility = Visibility.Visible;
                        usersBtn.Visibility = Visibility.Collapsed;
                        break;
                    }
                case 2:
                    {

                        AllPatients.Visibility = Visibility.Visible;
                        AllServices.Visibility = Visibility.Visible;
                        AllOrders.Visibility = Visibility.Visible;
                        ReportBtn.Visibility = Visibility.Visible;
                        LogBtn.Visibility = Visibility.Collapsed;
                        analyzerBtn.Visibility = Visibility.Collapsed;
                        usersBtn.Visibility = Visibility.Collapsed;
                        break;

                    }
                case 3:
                    {

                        AllPatients.Visibility = Visibility.Visible;
                        AllServices.Visibility = Visibility.Visible;
                        AllOrders.Visibility = Visibility.Visible;
                        ReportBtn.Visibility = Visibility.Visible;
                        LogBtn.Visibility = Visibility.Visible;
                        analyzerBtn.Visibility = Visibility.Visible;
                        usersBtn.Visibility = Visibility.Visible;
                        break;
                    }

                    /* 
id_role	name
1	Лаборант
2	Бухгалтер
3	Администратор
                     */
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientsList());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesList());

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Orders());

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {//photo
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Выберите фото"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ImageProfile.Source = SetImage(((MainWindow)Application.Current.MainWindow).user.photo= File.ReadAllBytes(openFileDialog.FileName));
            }

        }


        private BitmapImage SetImage(byte[] image)
        {
            try
            {
                Stream memoryStream = new MemoryStream(((MainWindow)Application.Current.MainWindow).user.photo);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                polikEntities6.GetContext().SaveChanges();
                return bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Изображение было повреждено, {ex.Message.ToString()}");
            }
            return null;
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportsList());
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogsList());

        }

        private void analyzerBtn_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AnalyzersList());
        }

        private void usersBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersList());

        }
    }
}
