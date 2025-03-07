using Poliklinika20.DB;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для LogsList.xaml
    /// </summary>
    public partial class LogsList : Page
    {
        public LogsList()
        {
            InitializeComponent();
            LogsDataGrid.ItemsSource = polikEntities6.GetContext().visit_logs.ToList();
        }

        private void LogsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {

            LogsDataGrid.ItemsSource = polikEntities6.GetContext().visit_logs.ToList();
        }

        private void ButtomDelLog_Click(object sender, RoutedEventArgs e)
        {
            if (LogsDataGrid.SelectedItem is visit_logs visit_log)
            {
                if (MessageBox.Show("Вы действительно хотите удалить лог?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        visit_log.is_archived = true;
                        polikEntities6.GetContext().SaveChanges();
                        if (LogsDataGrid != null)
                        LogsDataGrid.ItemsSource = polikEntities6.GetContext().visit_logs.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Выберите лог для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void ComboBoxFitlter_Selected(object sender, RoutedEventArgs e)
        {
            // Очищаем NavContainer, если есть дополнительные элементы


            switch (ComboBoxFitlter.SelectedIndex)
            {
                case 0:
                    if (LogsDataGrid != null)
                        LogsDataGrid.ItemsSource = polikEntities6.GetContext().visit_logs.ToList();
                    ComboBoxFitlterUsers.Visibility = Visibility.Collapsed;
                    break;

                case 1:
                    ComboBoxFitlterUsers.Visibility = Visibility.Visible;
                    ComboBoxFitlterUsers.DisplayMemberPath = "full_name";
                    ComboBoxFitlterUsers.ItemsSource = polikEntities6.GetContext().users.ToList();
                    ComboBoxFitlterUsers.SelectionChanged += (s, args) =>
                    {
                        if (ComboBoxFitlterUsers.SelectedItem is users selectedUser)
                        {
                            LogsDataGrid.ItemsSource = polikEntities6.GetContext().visit_logs
                                .Where(v => v.users.id_user == selectedUser.id_user)
                                .ToList();
                        }
                    };

                    break;

                case 2:
                    // Выводим только удаленные логи
                    ComboBoxFitlterUsers.Visibility = Visibility.Collapsed;
                    LogsDataGrid.ItemsSource = polikEntities6.GetContext().visit_logs
                        .Where(v => v.is_archived)
                        .ToList();
                    break;
            }
        }
    }
}
