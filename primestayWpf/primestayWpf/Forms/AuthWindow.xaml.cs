using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.src.auth;
using System.Windows;

namespace primestayWpf.Forms
{
    public partial class AuthWindow : Window
    {

        private readonly IDao<UserDto> dao;

        public AuthWindow(IDao<UserDto> _dao)
        {
            InitializeComponent();
            this.dao = _dao;
        }

        public void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(usernameField.Text) || string.IsNullOrEmpty(passwordField.Password))
            {
                errorLabel.Content = "Both username and password must be set!";
                return;
            }

            errorLabel.Content = "";
            var res = (dao as IDaoAuthExtension<UserDto>).login(usernameField.Text, passwordField.Password);

            if (res == null || string.IsNullOrEmpty(res.Token))
            {
                errorLabel.Content = "Username or password is invalid!";
            }
            else
            {
                Auth.AccessToken = res.Token;
                Auth.username = res.name;
                Close();
            }
        }
    }


}
