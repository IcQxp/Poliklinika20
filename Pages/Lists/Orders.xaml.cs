using Poliklinika20.DB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Poliklinika20.Pages.Lists
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private insurers_bills insurers_Bills = new insurers_bills();
        public Orders()
        {
            InitializeComponent();
            OrdersDataGrid.ItemsSource = polikEntities6.GetContext().orders.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var order = new orders();
            NavigationService.Navigate(new PatientsList(order));
            
        }

        private void OrdersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            OrdersDataGrid.ItemsSource = polikEntities6.GetContext().orders.ToList();

        }



        public Orders(users _user)
        {
            InitializeComponent();
            OrdersDataGrid.ItemsSource = polikEntities6.GetContext().orders.ToList();

            insurers_Bills.is_archived = false;
            insurers_Bills.creation_date = DateTime.Now;
            insurers_Bills.id_accountant = _user.id_user;
            AddOrderBtn.Visibility = Visibility.Collapsed;
            AddReportBtn.Visibility = Visibility.Visible;
        }

        private void AddReportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem is orders order)
            {
                try
                {
                    insurers_Bills.id_order = order.id_order;

                    polikEntities6.GetContext().insurers_bills.Add(insurers_Bills);
                    polikEntities6.GetContext().SaveChanges();
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
