using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using System.Windows;
using WinApp.Components.Authentication;
using WinApp.Components.CustomerViews;
using WinApp.Components.HotelViews;
using WinApp.Components.RoomTypeViews;
using WinApp.src.auth;

namespace WinApp.Components
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = RestDataContext.GetInstance();
            usernameLabel.Content = string.Format("Welcome, {0}", Auth.Username ?? "unknown");
        }


        private void hotelCrudBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.IsLoggedIn) new HotelMenu(DaoFactory.Create<HotelDto>(_context, Auth.AccessToken)).ShowDialog();
            else MessageBox.Show("Login to acces Hotels", "Error", MessageBoxButton.OK);
        }

        private void logOutbtn_click(object sender, RoutedEventArgs e)
        {
            Auth.Logout();
            new AuthWindow().Show();
            Close();
        }

        private void roomTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.IsLoggedIn) new RoomTypeMenu(DaoFactory.Create<RoomTypeDto>(_context, Auth.AccessToken)).ShowDialog();
            else MessageBox.Show("Login to access Room types", "Error", MessageBoxButton.OK);
        }

        private void customerCrudBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.IsLoggedIn) new CustomerMenu(DaoFactory.Create<CustomerDto>(_context, Auth.AccessToken)).ShowDialog();
            else MessageBox.Show("Login to access Customers", "Error", MessageBoxButton.OK);

        }

    }
}
