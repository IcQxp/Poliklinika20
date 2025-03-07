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
    /// Логика взаимодействия для AddEditAnalyzerPage.xaml
    /// </summary>
    public partial class AddEditAnalyzerPage : Page
    {
        private analyzers _analyzer;

        public AddEditAnalyzerPage()
        {
            InitializeComponent();
            _analyzer = new analyzers();
            DataContext = _analyzer;
        }

        public AddEditAnalyzerPage(analyzers analyzer) : this()
        {
            _analyzer = analyzer;
            DataContext = _analyzer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_analyzer.id_analyzer == 0)
            {
                polikEntities6.GetContext().analyzers.Add(_analyzer);
            }
            else
            {
                var existingAnalyzer = polikEntities6.GetContext().analyzers.Find(_analyzer.id_analyzer);
                if (existingAnalyzer != null)
                {
                    existingAnalyzer.name = _analyzer.name;
                    existingAnalyzer.description = _analyzer.description;
                    existingAnalyzer.last_maintenance_date = _analyzer.last_maintenance_date;
                    existingAnalyzer.manufacture_year = _analyzer.manufacture_year;
                    existingAnalyzer.is_archived = _analyzer.is_archived; 
                }
            }

            polikEntities6.GetContext().SaveChanges();
            NavigationService.GoBack();
        }
    }
}
