using Poliklinika20.DB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Poliklinika20.Pages.Lists
{
    /// <summary>
    /// Логика взаимодействия для ReportsList.xaml
    /// </summary>
    public partial class ReportsList : Page
    {
        private users _user = ((MainWindow)Application.Current.MainWindow).user;

        public ReportsList()
        {
            InitializeComponent();
            ReportsDataGrid.ItemsSource = polikEntities6.GetContext().insurers_bills.ToList();
            
        }

        private void OrdersDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ReportsDataGrid.ItemsSource = polikEntities6.GetContext().insurers_bills.ToList();

        }

        private void CreateBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Orders(_user));
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ReportsDataGrid.SelectedItem is insurers_bills insurer_Bill)
            {
                if (MessageBox.Show("Вы действительно хотите удалить отчет?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        insurer_Bill.is_archived = true;
                        polikEntities6.GetContext().SaveChanges();
                        ReportsDataGrid.ItemsSource = polikEntities6.GetContext().insurers_bills.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Выберите отчет для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
