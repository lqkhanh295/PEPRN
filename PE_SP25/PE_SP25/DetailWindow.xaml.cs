using BLL.Service;
using DAL;
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
using System.Windows.Shapes;

namespace PE_SP25
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {

        public Inventory EditedMoc { get; set; }
        private InventoryService _service = new();
        private PlantService _plantService = new();
        public DetailWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
                Inventory x = new();
                x.InventoryId = int.Parse(IDTextBox.Text);
                x.Quantity = int.Parse(QuantityTextBox.Text);
                x.Price = decimal.Parse(PriceTextBox.Text);
                x.ArrivalDate = DateOnly.Parse(DatePick.Text);
                x.ShelfLife = int.Parse(SheflLifeTextBox.Text);
                x.Supplier = SupplierTextBox.Text;
                x.PlantId = int.Parse(PlantIdComboBox.SelectedValue.ToString());

                if (EditedMoc == null)
                {
                    _service.AddInventory(x);
                }
                else
                {
                    _service.UpdateInventory(x);
                }
                this.Close();
        }

        private void FillComboBox()
        {
            PlantIdComboBox.ItemsSource = _plantService.GetAllPlants();
            PlantIdComboBox.DisplayMemberPath = "Name";
            PlantIdComboBox.SelectedValuePath = "PlantId";
        }

        private void FillElement(Inventory x)
        {
            if (x == null) return;
            IDTextBox.Text = x.InventoryId.ToString();
            QuantityTextBox.Text = x.Quantity.ToString();
            PriceTextBox.Text = x.Price.ToString();
            DatePick.Text = x.ArrivalDate.ToString();
            SheflLifeTextBox.Text = x.ShelfLife.ToString();
            SupplierTextBox.Text = x.Supplier;
            PlantIdComboBox.SelectedValue = x.PlantId;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillElement(EditedMoc);
            FillComboBox();

            if(EditedMoc != null)
            {
                IDTextBox.IsReadOnly = true;
                IDTextBox.Background = System.Windows.Media.Brushes.LightGray;
            }
        }
    }
}
