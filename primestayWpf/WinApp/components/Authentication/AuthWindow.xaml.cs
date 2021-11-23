using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using System.Threading.Tasks;
using System.Windows;
using WinApp.src.auth;

namespace WinApp.Components.Authentication
{
    public partial class AuthWindow : Window
    {

        private readonly IDao<UserDto> dao;

        public AuthWindow()
        {
            InitializeComponent();

            var _context = RestDataContext.GetInstance();
            dao = DaoFactory.Create<UserDto>(_context, Auth.AccessToken);

            usernameField.Focus();
        }

        public async void loginBtn_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(usernameField.Text) || string.IsNullOrEmpty(passwordField.Password))
            {
                errorLabel.Content = "Both username and password must be set!";
                return;
            }

            loadingBar.Visibility = Visibility.Visible;
            await callLogin(usernameField.Text, passwordField.Password);
            loadingBar.Visibility = Visibility.Hidden;

            errorLabel.Content = "";
            
        }

        private async Task callLogin(string username, string password)
        {
            //Start a new thread, in order to not block STA (UI thread)
            var res =  await Task<UserDto>.Factory.StartNew(() => 
            ((IDaoAuthExtension<UserDto>)dao)
            .login(username, password));
            
            if (res == null || string.IsNullOrEmpty(res.Token))
            {
                errorLabel.Content = "Username or password is invalid!";
            }
            else
            {
                Auth.AccessToken = res.Token;
                Auth.Username = res.name ?? "admin";
                new MainWindow().Show();
                Close();
            } 
            
        }

        private void passwordField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key is System.Windows.Input.Key.Enter) loginBtn_Click(sender, e);
        }
    }


}
