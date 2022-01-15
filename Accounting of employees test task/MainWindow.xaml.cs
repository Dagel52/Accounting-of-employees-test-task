using System.Windows;

namespace Accounting_of_employees_test_task
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel(new DefaultDialogService(), new JsonFileService());
        }
    }
}
