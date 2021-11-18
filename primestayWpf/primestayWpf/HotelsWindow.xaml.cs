using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.HotelCRUD;
using PrimestayWpf.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace primestayWpf
{
    /// <summary>
    /// Interaction logic for HotelsWindow.xaml
    /// </summary>
    public partial class HotelsWindow : Window
    {
        private readonly IDao<HotelDto> dao;
        private ObservableCollection<Hotel> hotelList { get; set; } = new ObservableCollection<Hotel>();

        public HotelsWindow(IDao<HotelDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            var hotels = dao.ReadAll(new HotelDto());
            hotels.ToList().ForEach(hotel => hotelList.Add(hotel));
            HotelListView.ItemsSource = hotelList;
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            var oldHotel = HotelListView.SelectedItem as Hotel;
            if (oldHotel is null) MessageBox.Show("Please select a Hotel to edit", "ERROR");
            else
            {
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

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var Hotel = HotelListView.SelectedItem as Hotel;
            if (Hotel is null) MessageBox.Show("Please select a Hotel to delete", "ERROR");
            else
            {
                string text = $"Are you sure you would like to delete {Hotel.Name}?";
                if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    dao.Delete(Hotel);
                }
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
