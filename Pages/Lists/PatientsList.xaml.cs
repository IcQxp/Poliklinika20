using Poliklinika20.DB;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Poliklinika20.Pages.Lists
{
    /// <summary>
    /// Логика взаимодействия для PatientsList.xaml
    /// </summary>
    public partial class PatientsList : Page
    {
            private orders _order;
        public PatientsList()
        {
            InitializeComponent();
            PatientsDataGrid.ItemsSource = polikEntities6.GetContext().patients.ToList();
        }

        public PatientsList(orders order)
        {
            InitializeComponent();
            _order = order;
            SelectBtn.Visibility = System.Windows.Visibility.Visible;
            PatientsDataGrid.ItemsSource = polikEntities6.GetContext().patients.ToList();
        }

        private void SelectBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var pat = PatientsDataGrid.SelectedItem as patients;
            if (pat == null)
            {
                MessageBox.Show("Выберите клиента");
                
            }else
            {
                NavigationService.Navigate(new CompletedServices(_order, pat));

            }

        }
    }
}
