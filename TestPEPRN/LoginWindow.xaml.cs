using System.Windows;
using System.Windows.Controls;
using TestPEPRN.BLL.Services;
using TestPEPRN.DAL.Models;

namespace TestPEPRN
{
    public partial class LoginWindow : Window
    {
        private readonly MemberService _memberService = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox? emailTextBox = FindName("EmailTextBox") as TextBox;
            PasswordBox? passwordBox = FindName("PasswordTextBox") as PasswordBox;

            string email = emailTextBox?.Text?.Trim() ?? string.Empty;
            string password = passwordBox?.Password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            StaffMember? account = _memberService.Authenticate(email.ToLowerInvariant(), password.ToLowerInvariant());
            if (account == null)
            {
                MessageBox.Show("Invalid email address or wrong password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (account.Role == 3)
            {
                MessageBox.Show("You do not have permission to access this application.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MainWindow m = new();
            m.CurrentAccount = account;
            m.Show();
            this.Hide();
        }
    }
}
