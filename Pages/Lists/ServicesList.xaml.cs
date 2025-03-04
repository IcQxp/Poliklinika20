
using Poliklinika20.DB;
using System.Linq;
using System.Windows.Controls;

namespace Poliklinika20.Pages.Lists
{
    /// <summary>
    /// Логика взаимодействия для ServicesList.xaml
    /// </summary>
    public partial class ServicesList : Page
    {
        public ServicesList()
        {
            InitializeComponent();
            SErvicesDataGrid.ItemsSource = polikEntities6.GetContext().services.ToList();
        }
    }
}
