using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.HotelCRUD;
using PrimestayWpf.Model;
using PrimestayWPF.DataAccessLayer.DTO;
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
            HotelListView.ItemsSource = hotelList;
            UpdateList();
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
                        href = form.HotelHref,
                        Name = form.Name.Text,
                        Description = form.Description.Text,
                        LocationHref = form.LocationHref.Text,
                        StaffedHours = form.StaffedHours.Text,
                        Stars = (int)form.Stars.Value,
                    };
                    var res = dao.Update(hotel.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Hotel {hotel.Name} was updated");
                    else MessageBox.Show($"Could not update {hotel.Name}, contact admin");
                }
            }

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var hotel = HotelListView.SelectedItem as Hotel;
            string text = $"Are you sure you would like to delete {hotel?.Name ?? "this hotel"}?";
            if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (MessageBox.Show($"Are you sure you would like to delete {hotel.Name}?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(hotel.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Hotel {hotel.Name} was deleted");
                    else MessageBox.Show($"Could not delete {hotel.Name}, contact admin");
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
                var newHotelHref = dao.Create(hotel.Map());
                if (newHotelHref is null) MessageBox.Show("could not create Hotel");
                else
                {
                    MessageBox.Show($"Hotel: {hotel.Name} was succesfully created");
                    UpdateList();
                }
            }

        }

        private void UpdateList()
        {
            var hotels = dao.ReadAll(new HotelDto()).Select(x => x.Map());
            hotelList.Clear();
            hotels.ToList().ForEach(x => hotelList.Add(x));
        }

    }
}
