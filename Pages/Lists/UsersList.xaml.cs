using Poliklinika20.DB;
using Poliklinika20.Pages.Edit;
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
    /// Логика взаимодействия для UsersList.xaml
    /// </summary>
    public partial class UsersList : Page
    {
        public UsersList()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            UsersDataGrid.ItemsSource = polikEntities6.GetContext().users.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addPage = new AddEditUserPage();
            NavigationService.Navigate(addPage);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is users selectedUser)
            {
                
                NavigationService.Navigate(new AddEditUserPage(selectedUser));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is users selectedUser)
            {
                selectedUser.is_archived = true;
                polikEntities6.GetContext().SaveChanges();
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
        }
    }
}
