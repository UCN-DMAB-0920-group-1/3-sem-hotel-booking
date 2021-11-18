using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.HotelCRUD;
using PrimestayWpf.Model;
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

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(usernameField.Text) || string.IsNullOrEmpty(passwordField.Password))
            {
                MessageBox.Show("Both username and password must be set!", "Invalid inputs");
                return;
            }


            var res = (dao as IDaoAuthExtension<UserDto>).login(usernameField.Text, passwordField.Password);

        }
    }
}
