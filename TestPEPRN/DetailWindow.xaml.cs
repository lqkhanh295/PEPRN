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
using TestPEPRN.BLL.Services;
using TestPEPRN.DAL.Models;

namespace TestPEPRN
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {

        private AirConService _service = new(); 
        private SupplierService _supplierService = new();   

        public AirConditioner EdittedAirCon { get; set; }// biến flag, biến cờ phất lên tùy ngữ cảnh, nếu nó kh được phất lên nghĩa là null, màn hình tạo mới, nếu biến này phất lên = máy lạnh nào đó có săn (đc truyền từ bên grid đem sang), do user chọn dòng muốn edit => EdittedAirCon

        public DetailWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AirConditioner x = new();
            x.AirConditionerId = int.Parse(AirConditionerIDTextBox.Text);
            x.AirConditionerName = AirConditionerNameTextBox.Text;
            x.Warranty = WarrantyTextBox.Text;
            x.SoundPressureLevel = SoundLevelTextBox.Text;
            x.FeatureFunction = FeatureFunctionTextBox.Text;
            x.Quantity = int.Parse(QuantityTextBox.Text);
            x.DollarPrice = float.Parse(PriceTextBox.Text);
            x.SupplierId = SupplierIDComboBox.SelectedValue.ToString();

            // Check if the ID already exists
            
            if (EdittedAirCon == null)
            {
                // Update existing record
                _service.CreateAirConditioner(x);
            }
            else
            {
                // Add new record
                _service.UpdateAirConditioner(x);
            }
            
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            FillElement(EdittedAirCon);
            if(EdittedAirCon != null)
            {
                DetailWindowLabel.Content = "Update Air Conditioner";
            }else
            {
                DetailWindowLabel.Content = "Create New Air Conditioner";
            }
        }

        private void FillElement(AirConditioner x)
        {
            if (x == null) return;
            AirConditionerIDTextBox.Text = x.AirConditionerId.ToString();
            AirConditionerIDTextBox.IsEnabled = false; //không cho sửa khóa chính
            AirConditionerNameTextBox.Text = x.AirConditionerName;
            WarrantyTextBox.Text = x.Warranty;
            SoundLevelTextBox.Text = x.SoundPressureLevel;
            FeatureFunctionTextBox.Text = x.FeatureFunction;
            QuantityTextBox.Text = x.Quantity.ToString();
            PriceTextBox.Text = x.DollarPrice.ToString();
            //nhảy đến đùng lựa chọn trong danh sách 5 nhà cung cấp
            //cái lựa chọn này đang có sẵn trong khóa ngoại đọc từ table lên
            SupplierIDComboBox.SelectedValue = x.SupplierId;

        }

        private void FillComboBox()
        {
            SupplierIDComboBox.ItemsSource = _supplierService.GetAllSuppliers();
            //show name collum
            SupplierIDComboBox.DisplayMemberPath = "SupplierName";
            SupplierIDComboBox.SelectedValuePath = "SupplierId";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
