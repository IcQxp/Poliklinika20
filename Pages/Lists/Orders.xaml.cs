using Poliklinika20.DB;
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
    }
}
