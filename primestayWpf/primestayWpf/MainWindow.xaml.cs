using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using System.Windows;
using primestayWpf.Forms;

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
            new HotelMenu(DaoFactory.Create<HotelDto>(_context)).ShowDialog();
        }

        private void authScreenbtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthWindow(DaoFactory.Create<UserDto>(_context)).ShowDialog();
        }

        private void roomTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            new RoomTypeWindow(DaoFactory.Create<RoomTypeDto>(_context)).ShowDialog();
        }
    }
}
