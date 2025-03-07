using Poliklinika20.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Poliklinika20.Pages.Lists
{
    /// <summary>
    /// Логика взаимодействия для CompletedServices.xaml
    /// </summary>
    public partial class CompletedServices : Page
    {
        ObservableCollection<completed_services> list;
        private orders _order;
        private patients _patient;

        public CompletedServices()
        {
            InitializeComponent();
        }

        public CompletedServices(orders order, patients patient)
        {
            InitializeComponent();
            _order = order;
            _patient = patient;
            polikEntities6 lr = polikEntities6.GetContext();
            UserDataGrid.ItemsSource = lr.users.ToList();
            AnalyserDataGrid.ItemsSource = lr.analyzers.ToList();
            ServicesDataGrid.ItemsSource = lr.services.ToList();
            list = new ObservableCollection<completed_services>();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var service = ServicesDataGrid.SelectedItem as services;
            var analyzer = AnalyserDataGrid.SelectedItem as analyzers;
            var user = UserDataGrid.SelectedItem as users;

            if (service == null || analyzer == null || user == null)
                MessageBox.Show("Для добавления необходимо выбрать новые значения");
            else
            {
                var compl = new completed_services();
                compl.users = user;
                compl.services = service;
                compl.analyzers = analyzer;
                compl.orders = _order;


                list.Add(compl);
                Console.Write(list.Count);
                CompletedServicesDataGrid.ItemsSource = list;
                //CompletedServicesDataGrid.DataContext = list;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var compl = CompletedServicesDataGrid.SelectedItem as completed_services;
            if (compl == null)
            {
                MessageBox.Show("Для редактирования необходимо выбрать услугу заказа");
                return;
            }

            var service = ServicesDataGrid.SelectedItem as services;
            var analyzer = AnalyserDataGrid.SelectedItem as analyzers;
            var user = UserDataGrid.SelectedItem as users;

            if (service == null || analyzer == null || user == null)
            {
                MessageBox.Show("Для редактирования необходимо выбрать новые значения");
                return;
            }

            compl.users = user;
            compl.services = service;
            compl.analyzers = analyzer;

            CompletedServicesDataGrid.ItemsSource = null; 
            CompletedServicesDataGrid.ItemsSource = list;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (_order != null && list != null && list.Count > 0)
            {

                try
                {
                    decimal cost = 0;
                    foreach (var elem in list)
                    {
                        cost += elem.services.cost;
                    }
                    _order.total_cost = cost;
                    _order.id_patient = _patient.id_patient;
                    _order.status = "новый";
                    _order.creation_date = DateTime.Now;
                    polikEntities6.GetContext().orders.Add(_order);
                    polikEntities6.GetContext().SaveChanges();
                    var orderNow = polikEntities6.GetContext().orders
                        .Where(or => or.total_cost == cost && or.id_patient == _patient.id_patient)
                        .OrderByDescending(or => or.id_order)
                        .FirstOrDefault();
                    foreach (var elem in list)
                    {
                        elem.id_lab_technician = elem.users.id_user;
                        elem.id_order = orderNow.id_order;
                        elem.id_service = elem.services.id_service;
                        elem.id_analyzer = elem.analyzers.id_analyzer;
                        elem.service_status = "новая";
                        elem.completion_time_days = (short)(elem.services.completion_period_days + elem.services.average_deviation_days);


                        polikEntities6.GetContext().completed_services.Add(elem);

                    }
                    polikEntities6.GetContext().SaveChanges();
                    NavigationService.GoBack();
                    NavigationService.GoBack();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось добавить заказ в базу данных" + ex.Message);
                }
            }
        }
    }
}
