using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.Forms;
using System.Windows;

namespace primestayWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataContext _context;
        private bool IsLoggedIn = false;

        public MainWindow()
        {
            InitializeComponent();
            _context = RestDataContext.GetInstance();
        }


        private void hotelCrudBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoggedIn) new HotelMenu(DaoFactory.Create<HotelDto>(_context)).ShowDialog();
            else MessageBox.Show("Login to acces Hotels", "Error", MessageBoxButton.OK);
        }

        private void authScreenbtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthWindow(DaoFactory.Create<UserDto>(_context), this).ShowDialog();
        }

        private void roomTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoggedIn) new RoomTypeWindow(DaoFactory.Create<RoomTypeDto>(_context)).ShowDialog();
            else MessageBox.Show("Login to acces Room types", "Error", MessageBoxButton.OK);
        }

        internal void Login()
        {
            IsLoggedIn = true;
        }
    }
}
