using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.HotelCRUD;
using PrimestayWpf.Model;
using System.Windows;

namespace primestayWpf
{
    /// <summary>
    /// Interaction logic for HotelsWindow.xaml
    /// </summary>
    public partial class HotelsWindow : Window
    {
        private readonly IDao<HotelDto> dao;

        public HotelsWindow(IDao<HotelDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            var hotels = dao.ReadAll(new HotelDto());
            DataGrid1.ItemsSource = hotels;
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            var oldHotel = DataGrid1.SelectedItem as HotelDto;
            var form = oldHotel is null ? new HotelForm() : new HotelForm(oldHotel);
            var yesNo = form.ShowDialog();
            if (yesNo ?? false)
            {
                Hotel hotel = new()
                {
                    Name = form.Name.Text,
                    Description = form.Description.Text,
                    LocationHref = form.LocationHref.Text,
                    StaffedHours = form.StaffedHours.Text,
                    Stars = (int)form.Stars.Value,
                };
                dao.Update(hotel);
            }

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var oldHotel = DataGrid1.SelectedItem as HotelDto;
            string text = $"Are you sure you would like to delete {oldHotel.Name}?";
            if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                dao.Delete(oldHotel);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var form = new HotelForm();
            var yesNo = form.ShowDialog();
            if (yesNo ?? false)
            {
                Hotel hotel = new()
                {
                    Name = form.Name.Text,
                    Description = form.Description.Text,
                    LocationHref = form.LocationHref.Text,
                    StaffedHours = form.StaffedHours.Text,
                    Stars = (int)form.Stars.Value,
                };
                Create(hotel);
            }

        }

        public string Create(Hotel hotel)
        {
            return dao.Create(hotel);

        }
    }
}
