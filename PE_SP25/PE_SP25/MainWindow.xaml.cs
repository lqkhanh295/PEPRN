using BLL.Service;
using DAL;
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

namespace PE_SP25
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private InventoryService _service = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void FillDataGrid()
        {
            InventoryDataGrid.ItemsSource = null;
            InventoryDataGrid.ItemsSource = _service.GetAllInventories();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailWindow d = new();
            d.ShowDialog();
            FillDataGrid(); // Refresh data after DetailWindow closes
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Inventory? selected = InventoryDataGrid.SelectedItem as Inventory;
            if(selected == null)
            {
                MessageBox.Show("Please select an inventory item to update.");
                return;
            }
            DetailWindow d = new();
            d.EditedMoc = selected;
            d.ShowDialog();
            FillDataGrid(); // Refresh data after DetailWindow closes
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Inventory? selected = InventoryDataGrid.SelectedItem as Inventory;
            if(selected == null)
            {
                MessageBox.Show("Please choose one");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected plant?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _service.DeleteInventory(selected);
                FillDataGrid();
            }
        }
    }
}