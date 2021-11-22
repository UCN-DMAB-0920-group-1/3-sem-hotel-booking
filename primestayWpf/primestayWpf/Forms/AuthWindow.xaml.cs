using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.src.auth;
using System.Windows;

namespace primestayWpf.Forms
{
    public partial class AuthWindow : Window
    {

        private readonly IDao<UserDto> dao;
        private readonly MainWindow parrent;

        public AuthWindow(IDao<UserDto> _dao, MainWindow parrent)
        {
            InitializeComponent();
            this.dao = _dao;
            this.parrent = parrent;
            usernameField.Focus();
        }

        public void loginBtn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(usernameField.Text) || string.IsNullOrEmpty(passwordField.Password))
            {
                errorLabel.Content = "Both username and password must be set!";
                return;
            }

            errorLabel.Content = "";
            var res = (dao as IDaoAuthExtension<UserDto> ?? throw new System.Exception()).login(usernameField.Text, passwordField.Password);

            if (res == null || string.IsNullOrEmpty(res.Token))
            {
                errorLabel.Content = "Username or password is invalid!";
            }
            else
            {
                Auth.AccessToken = res.Token;
                Auth.username = res.name;
                parrent.Login();
                Close();
            }
        }

        private void passwordField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key is System.Windows.Input.Key.Enter) loginBtn_Click(sender, e);
        }
    }


}
