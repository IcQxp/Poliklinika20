using Poliklinika20.DB;
using Poliklinika20.Pages.Edit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Poliklinika20.Pages.Lists
{
    /// <summary>
    /// Логика взаимодействия для AnalyzersList.xaml
    /// </summary>
    public partial class AnalyzersList : Page
    {
        public AnalyzersList()
        {
            InitializeComponent();
            LoadAnalyzers();
        }

        private void LoadAnalyzers()
        {
            AnalyzersDataGrid.ItemsSource = polikEntities6.GetContext().analyzers.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addPage = new AddEditAnalyzerPage();
            NavigationService.Navigate(addPage);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnalyzersDataGrid.SelectedItem is analyzers selectedAnalyzer)
            {
                var editPage = new AddEditAnalyzerPage(selectedAnalyzer);
                NavigationService.Navigate(editPage);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите анализатор для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnalyzersDataGrid.SelectedItem is analyzers selectedAnalyzer)
            {
                selectedAnalyzer.is_archived = true;
                polikEntities6.GetContext().SaveChanges();
                LoadAnalyzers();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите анализатор для удаления.");
            }
        }

        private void AnalyzersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AnalyzersDataGrid.ItemsSource = polikEntities6.GetContext().analyzers.ToList();

        }
    }
}

