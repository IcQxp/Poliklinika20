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

namespace Poliklinika20.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для AddEditUserPage.xaml
    /// </summary>
    public partial class AddEditUserPage : Page
    {
        private users _user;

        public AddEditUserPage()
        {
            InitializeComponent();
            _user = new users();
            RoleComboBox.ItemsSource = polikEntities6.GetContext().roles.ToList();
            DataContext = _user;
        }

        public AddEditUserPage(users user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;
            if (RoleComboBox!=null)
            RoleComboBox.ItemsSource = polikEntities6.GetContext().roles.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_user.login))
            {
                MessageBox.Show("Логин не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(_user.password))
            {
                MessageBox.Show("Пароль не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(_user.last_name))
            {
                MessageBox.Show("Фамилия не может быть пустой.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(_user.first_name))
            {
                MessageBox.Show("Имя не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_user.id_role == 0) 
            {
                MessageBox.Show("Пожалуйста, выберите роль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_user.id_user == 0)
            {
                polikEntities6.GetContext().users.Add(_user);
            }
            else
            {
                var existingUser = polikEntities6.GetContext().users.Find(_user.id_user);
                if (existingUser != null)
                {
                    existingUser.login = _user.login;
                    existingUser.password = _user.password;
                    existingUser.last_name = _user.last_name;
                    existingUser.first_name = _user.first_name;
                    existingUser.patronymic = _user.patronymic;
                    existingUser.photo = _user.photo;
                    existingUser.is_archived = _user.is_archived; 
                }
            }

            polikEntities6.GetContext().SaveChanges();

            NavigationService.GoBack();
        }

        
    }
}
