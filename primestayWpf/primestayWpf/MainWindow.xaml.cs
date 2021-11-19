using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new HotelsWindow(DaoFactory.Create<HotelDto>(_context)).ShowDialog();
        }
    }
}
