using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.Forms;
using primestayWpf.src.auth;
using System.Windows;

namespace primestayWpf
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
        }


        private void hotelCrudBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.IsLoggedIn) new HotelMenu(DaoFactory.Create<HotelDto>(_context)).ShowDialog();
            else MessageBox.Show("Login to acces Hotels", "Error", MessageBoxButton.OK);
        }

        private void authScreenbtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthWindow(DaoFactory.Create<UserDto>(_context)).ShowDialog();
        }

        private void roomTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.IsLoggedIn) new RoomTypeMenu(DaoFactory.Create<RoomTypeDto>(_context)).ShowDialog();
            else MessageBox.Show("Login to access Room types", "Error", MessageBoxButton.OK);
        }
    }
}
