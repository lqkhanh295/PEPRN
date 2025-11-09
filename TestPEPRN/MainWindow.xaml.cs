using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestPEPRN.BLL.Services;
using TestPEPRN.DAL.Models;

namespace TestPEPRN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private AirConService _service =  new();

        public StaffMember CurrentAccount { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
            if(CurrentAccount.Role == 2)
            {
                CreateButtonClick.IsEnabled = false;
                UpdateButtonClick.IsEnabled = false;
                DeleteButtonClick.IsEnabled = false;
            }
        }

        private void FillDataGrid() 
        {
            AirConsDataGrid.ItemsSource = null;
            AirConsDataGrid.ItemsSource = _service.GetAllAirConditioners();
        }

        private void CreateButtonClick_Click(object sender, RoutedEventArgs e)
        {
            DetailWindow d = new();
            d.ShowDialog();
            FillDataGrid();
        }

        private void UpdateButtonClick_Click(object sender, RoutedEventArgs e)
        {
            //AirConsDataGrid.SelectedItem
            AirConditioner ? selected = AirConsDataGrid.SelectedItem as AirConditioner;
            if(selected == null)
            {
                MessageBox.Show("Please select an air conditioner to update.","Selected a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            DetailWindow d = new();
            d.EdittedAirCon = selected;
            d.ShowDialog();
            FillDataGrid();
        }

        private void DeleteButtonClick_Click(object sender, RoutedEventArgs e)
        {
            AirConditioner? selected = AirConsDataGrid.SelectedItem as AirConditioner;
            if (selected == null)
            {
                MessageBox.Show("Please select an air conditioner to update.", "Selected a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            MessageBoxResult answer = MessageBox.Show("Are you sure to delete this air conditioner?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                _service.DeleteAirConditioner(selected);
                FillDataGrid();
            }
            else
            {
                return;
            }
        }

        private void SearchButtonClick_Click(object sender, RoutedEventArgs e)
        {

            int ? quantity = null;
            int tmpQuantity;
            bool quantStatus = int.TryParse(QuantityTextbox.Text, out tmpQuantity);
            if(!quantStatus)
                MessageBox.Show("Incorrect quantity input, please enter a valid integer number.","Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                quantity = tmpQuantity;

            var result = _service.SearchAirConditioners(FunctionTextbox.Text, quantity);
            AirConsDataGrid.ItemsSource = null;
            AirConsDataGrid.ItemsSource = result;
        }

        private void QuitButtonClick_Click(object sender, RoutedEventArgs e)
        {
           Application.Current.Shutdown();
        }
    }
} 